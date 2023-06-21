using BizHawk.Client.Common;
using BizHawk.Common;
using BizHawk.Emulation.Common;
using HarmonyLib;
using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace BizhawkRandomizeInputs
{
    [HarmonyPatch(typeof(Controller))]
    static internal class JController
    {
        static private BizhawkRandomizeInputs? RandomizerInstance = null;

        static private ConstructorInfo? ctorMethod = null;
        static private MethodInfo? replacedCtor = null;

        static private MethodInfo? origLatch = null;
        static private MethodInfo? replacedLatch = null;

        static private RandomController? PlayerControllers = null;



        static public void Patch(Harmony harmony)
        {
            Type[] ctorParams = { typeof(ControllerDefinition) };

            ctorMethod = AccessTools.Constructor(typeof(Controller), ctorParams);

            if (ctorMethod != null)
                replacedCtor = harmony.Patch(ctorMethod, new HarmonyMethod(typeof(JController).GetMethod("ControllerCtor")));
            else
                Console.WriteLine("Failed to patch Ctor");

            origLatch = AccessTools.Method(typeof(Controller), "LatchFromPhysical");

            if (origLatch != null)
                replacedLatch = harmony.Patch(origLatch, new HarmonyMethod(typeof(JController).GetMethod("LatchFromPhysical")));
            else
                Console.WriteLine("Failed to patch LatchFromPhysical");
        }



        static public void Unpatch(Harmony harmony)
        {
            if (ctorMethod != null && replacedCtor != null)
                harmony.Unpatch(ctorMethod, replacedCtor);

            if (origLatch != null && replacedLatch != null)
                harmony.Unpatch(origLatch, replacedLatch);
        }



        static public void SetInputRandomizerInstance(BizhawkRandomizeInputs Randomizer)
        {
            RandomizerInstance = Randomizer;
        }



        /// <summary>
        /// Prefix patch of the Controller constructor
        /// Enumerates all lists in definition controls, finding player control lists
        /// </summary>
        /// <param name="definition"></param>
        static public void ControllerCtor(ControllerDefinition definition)
        {
            IReadOnlyList<IReadOnlyList<string>> contButtons = definition.ControlsOrdered.Where(
                    x => x.Count > 0 && x[0].Length >= 2 && x[0][0] == 'P' && Char.IsNumber(x[0][1])
                ).ToList();

            if (contButtons.Count > 0)
            {
                PlayerControllers = new RandomController(contButtons);
                RandomizerInstance!.ControllerButtons = PlayerControllers;
            }
        }



        static public bool LatchFromPhysical(
            ref WorkingDictionary<string, List<string>> ____bindings,
            ref WorkingDictionary<string, bool> ____buttons,
            ref WorkingDictionary<string, int> ____axes,
            ref Dictionary<string, AxisSpec> ____axisRanges,
            ref Dictionary<string, AnalogBind> ____axisBindings,
            ref IController finalHostController
        )
        {
            ____buttons.Clear();

            foreach (KeyValuePair<string, List<string>> binding in (Dictionary<string, List<string>>)____bindings)
            {
                binding.Deconstruct(out string key, out List<string> stringList);

                string randomizedKey = key;

                if (PlayerControllers != null && PlayerControllers.FlatRemappings.ContainsKey(key))
                    randomizedKey = PlayerControllers.FlatRemappings[key];

                ____buttons[randomizedKey] = false;

                foreach (string button in stringList)
                {
                    if (finalHostController.IsPressed(button))
                    {
                        ____buttons[randomizedKey] = true;
                        //Console.WriteLine("Latch From Physical: Button is Pressed [" + button + "] -> [" + key + "] -> [" + randomizedKey + "]");
                    }
                }
            }

            foreach (KeyValuePair<string, AnalogBind> axisBinding in ____axisBindings)
            {
                axisBinding.Deconstruct(out string key, out AnalogBind analogBind);

                float axisAlpha = finalHostController.AxisValue(analogBind.Value) / 10000f;
                float deadzone = analogBind.Deadzone;
                float deadzonedAlpha = (axisAlpha >= -deadzone ? (axisAlpha >= deadzone ? axisAlpha - deadzone : 0.0f) : axisAlpha + deadzone) / (1f - deadzone) * analogBind.Mult;
                
                AxisSpec axisRange = ____axisRanges[key];

                float range = deadzonedAlpha * Math.Max(axisRange.Neutral - axisRange.Min, axisRange.Max - axisRange.Neutral) + axisRange.Neutral;
                ____axes[key] = ((int)range).ConstrainWithin(axisRange.Range);
            }

            return false;
        }

    }
}
