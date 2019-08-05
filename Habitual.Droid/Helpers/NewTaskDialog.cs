using Android.Views;
using Android.Widget;
using Habitual.Core.Entities;
using Habitual.Core.Entities.Base;
using Habitual.Storage.Local;

namespace Habitual.Droid.Helpers
{
    public static class NewTaskDialog
    {
        private static View newTaskView;

        public static View SetupDialogFunctionality(View view)
        {
            newTaskView = view;

            newTaskView.FindViewById<LinearLayout>(Resource.Id.routineTimingLayout).Visibility = ViewStates.Gone;
            newTaskView.FindViewById<TextView>(Resource.Id.routineDaysActiveText).Visibility = ViewStates.Gone;

            HandleAppearanceOfRoutineDaySelection();

            return view;
        }

        private static void HandleAppearanceOfRoutineDaySelection()
        {
            var radioGroup = newTaskView.FindViewById<RadioGroup>(Resource.Id.taskTypeGroup);
            radioGroup.CheckedChange += TaskTypeChanged;
        }

        private static void TaskTypeChanged(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            var routineRadioButton = newTaskView.FindViewById<RadioButton>(Resource.Id.routineRadioSelection);
            var routineDaysContainer = newTaskView.FindViewById<LinearLayout>(Resource.Id.routineTimingLayout);
            var routineDaysText = newTaskView.FindViewById<TextView>(Resource.Id.routineDaysActiveText);

            routineDaysContainer.Visibility = routineRadioButton.Checked ? ViewStates.Visible : ViewStates.Gone;
            routineDaysText.Visibility = routineRadioButton.Checked ? ViewStates.Visible : ViewStates.Gone;
        }

        public static BaseTask GenerateTaskFromDialog(View view)
        {
            var habitSelection = view.FindViewById<RadioButton>(Resource.Id.habitRadioSelection);
            var routineSelection = view.FindViewById<RadioButton>(Resource.Id.routineRadioSelection);
            var todoSelection = view.FindViewById<RadioButton>(Resource.Id.todoRadioSelection);

            if (habitSelection.Checked) return GenerateTask(view, new Habit()) as Habit;
            if (routineSelection.Checked) return GenerateRoutineFromDialog(view);
            return GenerateTask(view, new Todo()) as Todo; // default state
        }

        private static BaseTask GenerateTask(View view, BaseTask task)
        {
            task.Username = LocalData.Username;
            task.Description = view.FindViewById<TextView>(Resource.Id.descriptionNewTaskEntry).Text;
            task.Difficulty = GetDifficultyFromView(view);

            if (string.IsNullOrEmpty(task.Description)) task.Description = "Unnamed Task";

            return task;
        }

        private static BaseTask GenerateRoutineFromDialog(View view)
        {
            Routine routine = GenerateTask(view, new Routine()) as Routine;

            routine.IsActiveSunday = view.FindViewById<CheckBox>(Resource.Id.sundayCheckBox).Checked;
            routine.IsActiveMonday = view.FindViewById<CheckBox>(Resource.Id.mondayCheckBox).Checked;
            routine.IsActiveTuesday = view.FindViewById<CheckBox>(Resource.Id.tuesdayCheckBox).Checked;
            routine.IsActiveWednesday = view.FindViewById<CheckBox>(Resource.Id.wednesdayCheckBox).Checked;
            routine.IsActiveThursday = view.FindViewById<CheckBox>(Resource.Id.thursdayCheckBox).Checked;
            routine.IsActiveFriday = view.FindViewById<CheckBox>(Resource.Id.fridayCheckBox).Checked;
            routine.IsActiveSaturday = view.FindViewById<CheckBox>(Resource.Id.saturdayCheckBox).Checked;

            return routine;
        }

        private static Difficulty GetDifficultyFromView(View view)
        {
            var mediumSelection = view.FindViewById<RadioButton>(Resource.Id.mediumRadioSelection);
            var hardSelection = view.FindViewById<RadioButton>(Resource.Id.hardRadioSelection);
            var veryHardSelection = view.FindViewById<RadioButton>(Resource.Id.veryHardRadioSelection);

            if (mediumSelection.Checked) return Difficulty.Medium;
            if (hardSelection.Checked) return Difficulty.Hard;
            if (veryHardSelection.Checked) return Difficulty.VeryHard;
            return Difficulty.Easy; // Default state
        }
    }
}