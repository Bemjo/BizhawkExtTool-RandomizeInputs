using HarmonyLib;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System;
using BizHawk.Emulation.Common;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using WK.Libraries.HotkeyListenerNS;
namespace BizhawkRandomizeInputs
{
	[ExternalTool("BizhawkRandomizeInputs")]
    public partial class BizhawkRandomizeInputs : ToolFormBase, IExternalToolForm
	{
        static public readonly string ConfigFilePath = "randominputs.json";

		protected override string WindowTitleStatic => "Randomize Inputs";
        public ApiContainer? _maybeAPIContainer { get; set; }
        private ApiContainer APIs => _maybeAPIContainer!;

        private Harmony harmony;

        private string SystemName = "";

        private List<CheckedListBox> PlayerControlsAllowedLists = new List<CheckedListBox>();
        private List<ListView> PlayersRemappedControlList = new List<ListView>();

        private Dictionary<string, List<Dictionary<string, bool>>>? ConfigEntries = null;

        public RandomController? ControllerButtons = null;

        private HotkeyListener hkl;
        private HotkeySelector hks;
        private Hotkey randomizeHotkey;
        private Hotkey resetHotkey;

        private bool bRandomized = false;

        private string RandomizeHotkeyString = "Control+Shift+R";
        private string ResetHotkeyString = "Control+Shift+T";

        private class ConfigObj
        {
            string ResetHotkey = "Control+Shift+R";
            string RandomizeHotkey = "Control+Shift+T";
            Dictionary<string, List<Dictionary<string, bool>>>? SystemControllerButtonRandomizerWhitelist = null;
        }

        public BizhawkRandomizeInputs()
        {
            harmony = new Harmony("JBBizHawkRandomizeInputs" + Guid.NewGuid().ToString());
            JController.SetInputRandomizerInstance(this);
            JController.Patch(harmony);

            InitializeComponent();

            LoadJSONConfig();

            hkl = new HotkeyListener();
            hks = new HotkeySelector();

            randomizeHotkey = new Hotkey(RandomizeHotkeyString);
            resetHotkey = new Hotkey(ResetHotkeyString);

            hkl.Add(new[] { randomizeHotkey, resetHotkey });

            hkl.HotkeyPressed += HandleHotkeys;

            hks.Enable(textBox_RandomizeHotkey, randomizeHotkey);
            hks.Enable(textBox_ResetHotkey, resetHotkey);
        }



        ~BizhawkRandomizeInputs()
        {
            hkl.RemoveAll();
            //BuildConfigEntries();
            SaveJSONConfig();
        }



        private void HandleHotkeys(object sender, HotkeyEventArgs e)
        {
            if (e.Hotkey == randomizeHotkey)
                Randomize();
            else if (e.Hotkey == resetHotkey)
                Reset();
        }



        private void BuildConfigEntries()
        {
            if (ConfigEntries != null)
            {
                if (ConfigEntries.ContainsKey(SystemName))
                    ConfigEntries[SystemName].Clear();
            }
            else
            {
                ConfigEntries = new Dictionary<string, List<Dictionary<string, bool>>>();
            }
        }



        private void LoadJSONConfig()
        {
            if (!File.Exists(ConfigFilePath))
                return;

            using (StreamReader r = new StreamReader(ConfigFilePath))
            {
                string json = r.ReadToEnd();
                ConfigEntries = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, bool>>>>(json);
            }
        }



        private void SaveJSONConfig()
        {
            if (ConfigEntries != null && ConfigEntries.Count > 0)
            {
                using (StreamWriter w = new StreamWriter(ConfigFilePath, false))
                {
                    JsonConvert.SerializeObject(ConfigEntries);
                }
            }
        }



        public List<string>? GetEnabledRandomizedButtons(int Controller)
        {
            if (Controller >= PlayerControlsAllowedLists.Count)
                return null;

            CheckedListBox ControllerButtonsList = PlayerControlsAllowedLists[Controller];
            List<string> CanRandomizeButtonsList = new List<string>();

            for (int i = 0; i < ControllerButtonsList.Items.Count; ++i)
            {
                string key = ControllerButtonsList.GetItemText(ControllerButtonsList.Items[i]);

                if (ControllerButtonsList.GetItemChecked(i))
                    CanRandomizeButtonsList.Add(key);
            }

            return CanRandomizeButtonsList;
        }



        /// <summary>
        /// Called when a core is loaded, however this is called after the JController constructor patch executes
        /// Reset controller definitions and the UI
        /// </summary>
        public override void Restart()
        {
            IGameInfo? gameInfo = APIs?.Emulation.GetGameInfo();
            ClearTabPages();

            if (gameInfo != null && gameInfo.System.Length > 0 && gameInfo.System != "NULL")
            {
                SystemName = gameInfo.System;
                label_CoreName.Text = gameInfo.System;

                button_Randomize.Enabled = true;
                button_ResetControls.Enabled = true;

                BuildTabPages();
            }

            Reset();
        }



        private void BuildTabPages()
        {
            if (ControllerButtons == null)
                return;

            for (int i = 0; i < ControllerButtons.ButtonsPerController.Count; i++)
            {
                IReadOnlyList<string> Buttons = ControllerButtons.ButtonsPerController[i];

                Dictionary<string, bool> controllerButtons = new Dictionary<string, bool>();

                foreach (string Button in Buttons)
                {
                    controllerButtons.Add(Button, true);
                }

                CreateNewControllerTab(i, "Player " + (i+1), controllerButtons);

            }
        }



        private void ClearTabPages()
        {
            PlayerControlsAllowedLists.Clear();
            PlayersRemappedControlList.Clear();
            tabControl_ControllerButtonsLists.TabPages.Clear();
            tabControl_RemappedLists.TabPages.Clear();
        }



        private void CreateNewControllerTab(int controllerIndex, string TabName, Dictionary<string, bool> ButtonsList)
        {
            TabPage ChecklistPage = new TabPage(TabName);
            CheckedListBox buttonsListBox = new CheckedListBox();

            PlayerControlsAllowedLists.Add(buttonsListBox);

            buttonsListBox.CheckOnClick = true;
            buttonsListBox.FormattingEnabled = true;
            buttonsListBox.Location = new System.Drawing.Point(0, 0);
            buttonsListBox.Size = new System.Drawing.Size(tabControl_ControllerButtonsLists.Width-10, tabControl_ControllerButtonsLists.Height-20);
            // setup the check callback and assign an id as an index into various control lists
            buttonsListBox.ItemCheck += ButtonsListBox_ItemCheck;
            buttonsListBox.Tag = controllerIndex;

            foreach (var Button in ButtonsList)
            {
                buttonsListBox.Items.Add(Button.Key, Button.Value);
            }

            ChecklistPage.Controls.Add(buttonsListBox);

            tabControl_ControllerButtonsLists.TabPages.Add(ChecklistPage);

            TabPage ListPage = new TabPage(TabName);
            ListView remappedList = new ListView();

            ColumnHeader origHeader = new ColumnHeader();
            origHeader.Text = "Original";
            origHeader.Width = 236/2 - 10;

            ColumnHeader remappedHeader = new ColumnHeader();
            remappedHeader.Text = "Remapped";
            remappedHeader.Width = 236/2;

            remappedList.Columns.AddRange(new ColumnHeader[] { origHeader, remappedHeader });
            remappedList.FullRowSelect = true;
            remappedList.GridLines = true;
            remappedList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            remappedList.HideSelection = false;
            remappedList.Location = new System.Drawing.Point(0, 0);
            remappedList.MultiSelect = false;
            remappedList.Size = new System.Drawing.Size(tabControl_RemappedLists.Width-10, tabControl_RemappedLists.Height-20);
            remappedList.View = View.Details;

            PlayersRemappedControlList.Add(remappedList);

            ListPage.Controls.Add(remappedList);

            tabControl_RemappedLists.TabPages.Add(ListPage);   
        }



        private void ButtonsListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedListBox = sender as CheckedListBox;
            if (checkedListBox != null)
            {
                var item = ((CheckedListBox)sender).Items[e.Index] as string;

                if (item != null) {
                    ControllerButtons?.SetButtonRandomizeAllowed((int)checkedListBox.Tag, item, e.NewValue == CheckState.Checked);
                }
            }
        }



        private void ResetRemappingList()
        {
            if (ControllerButtons == null)
                return;

            for (int i = 0; i < ControllerButtons.ControllerCount; i++)
            {
                var mappings = ControllerButtons.Remappings(i);
                PlayersRemappedControlList[i].Items.Clear();

                foreach (var mapping in mappings!)
                {
                    PlayersRemappedControlList[i].Items.Add(mapping.Key).SubItems.Add(mapping.Value);
                }
            }
        }



        /// <summary>
        /// Randomizes the input for every controller and resets the UI controls for the new buttons
        /// </summary>
        private void Randomize()
        {
            // Ensure we've loaded a core first, and have something to randomize
            if (ControllerButtons != null && SystemName.Length > 0 && SystemName != "NULL")
            {
                ControllerButtons.RandomizeMappings(checkBox_UniqueRandom.Checked);
                ResetRemappingList();

                bRandomized = true;

                button_ResetControls.Enabled = true;
            }
        }



        private void Reset()
        {
            ControllerButtons?.ResetMappings();
            ResetRemappingList();
            button_ResetControls.Enabled = false;
            bRandomized = false;
        }
        


        private void button_Randomize_Click(object sender, EventArgs e)
        {
            Randomize();
        }



        private void button_ResetControls_Click(object sender, EventArgs e)
        {
            Reset();
        }


        public bool ValidateHotkey(TextBox? textbox, out string HotkeyText)
        {
            HotkeyText = (textbox == null) ? "" : textbox.Text;
            return textbox != null && !textbox.Text.Contains("None") && !textbox.Text.Contains("Unsupported");
        }


        private void textBox_RandomizeHotkey_TextChanged(object sender, EventArgs e)
        {
            if (ValidateHotkey(sender as TextBox, out string text))
            {
                hkl.Update(ref randomizeHotkey, HotkeyListener.Convert(text));
            }
        }

        private void textBox_ResetHotkey_TextChanged(object sender, EventArgs e)
        {
            if (ValidateHotkey(sender as TextBox, out string text))
            {
                hkl.Update(ref resetHotkey, HotkeyListener.Convert(text));
            }
        }
    }
}
