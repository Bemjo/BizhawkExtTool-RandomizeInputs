using System.Collections.Generic;
using System;
using System.Linq;
using MonoMod.Utils;

namespace BizhawkRandomizeInputs
{
    public class RandomController
    {
        // Arrray of controllers, with a button list for each controller
        public IReadOnlyList<IReadOnlyList<string>> ButtonsPerController { get; private set; }

        // Array of controllers, with a mapping from one button to another button
        private List<Dictionary<string, string>> RemappedInputs = new List<Dictionary<string, string>>();
        private List<Dictionary<string, bool>> AllowedToRandomize = new List<Dictionary<string, bool>>();

        // The flat packed list of remapped controller inputs, for use in JController latchToPhysical lookups
        public Dictionary<string, string> FlatRemappings { get; private set; } = new Dictionary<string, string>();

        private Random Generator = new Random();
        public int ControllerCount { get; private set; }




        public RandomController(IReadOnlyList<IReadOnlyList<string>> ButtonList)
        {
            ButtonsPerController = ButtonList;
            ControllerCount = ButtonList.Count;

            foreach (IReadOnlyList<string> buttons in ButtonsPerController)
            {
                AllowedToRandomize.Add(buttons.Zip(Enumerable.Repeat(true, buttons.Count), (a, b) => new { a, b }).ToDictionary(x => x.a, x => x.b));
            }

            ResetMappings();
        }



        public IReadOnlyDictionary<string, string>? Remappings(int controller)
        {
            if (controller < 0 || controller > RemappedInputs.Count)
                throw new IndexOutOfRangeException();

            return RemappedInputs[controller];
        }



        public void SetButtonRandomizeAllowed(int Controller, string Button, bool bRandomize)
        {
            if (Controller < 0 || Controller >= ButtonsPerController.Count)
                throw new IndexOutOfRangeException();

            var dict = AllowedToRandomize[Controller];

            if (dict.ContainsKey(Button))
                dict[Button] = bRandomize;
        }



        public void SetButtonRandomizeAllowed(int controller, Dictionary<string, bool> randomSettings)
        {
            if (controller < 0 || controller >= ButtonsPerController.Count)
                throw new IndexOutOfRangeException();

            foreach (var kvp in randomSettings)
            {
                SetButtonRandomizeAllowed(controller, kvp.Key, kvp.Value);
            }
        }



        public void ResetMapping(int controllerIndex)
        {
            if (controllerIndex < 0 || controllerIndex >= ButtonsPerController.Count)
                throw new IndexOutOfRangeException();

            var controller = ButtonsPerController[controllerIndex];
            var NeutralMappings = new Dictionary<string, string>(controller.ToDictionary(x => x, x => x));

            RemappedInputs.Add(NeutralMappings);
        }



        public void ResetMappings()
        {
            RemappedInputs.Clear();

            foreach (var controller in ButtonsPerController)
            {
                var NeutralMappings = new Dictionary<string, string>(controller.ToDictionary(x => x, x => x));

                RemappedInputs.Add(NeutralMappings);
            }

            RecreateFlatMapping();
        }



        private void RecreateFlatMapping()
        {
            FlatRemappings.Clear();

            RemappedInputs.ForEach(x => FlatRemappings.AddRange(x));
        }



        public void RandomizeMappings(bool EnsureUnique)
        {
            ResetMappings();

            for (int ControllerIndex = 0; ControllerIndex < ControllerCount; ++ControllerIndex)
            {
                List<string> enabledButtons = AllowedToRandomize[ControllerIndex].Where(kvp => kvp.Value).Select(kvp => kvp.Key).ToList();

                Dictionary<string, string> NewMappings = enabledButtons.Zip(ThreadSafeRandom.RandomizeList(enabledButtons, EnsureUnique), (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

                Dictionary<string, string> Remappings = RemappedInputs[ControllerIndex];

                // Merge dictionaries, keeping original control order
                RemappedInputs[ControllerIndex] = Remappings.Select(kvp => NewMappings.TryGetValue(kvp.Key, out string value) ? new KeyValuePair<string, string>(kvp.Key, value) : kvp).ToDictionary(x => x.Key, x => x.Value);
            }

            RecreateFlatMapping();
        }
    }
}
