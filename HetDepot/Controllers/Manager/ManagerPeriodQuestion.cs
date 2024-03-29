﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers.Manager;

public class ManagerPeriodQuestion : Controller
{
    public override void Execute()
    {
        var typeQuestion = new ListView<int>(Program.SettingService.GetConsoleText("managerTimeRangeQuestion"), new()
        {
            new ListViewItem<int>(Program.SettingService.GetConsoleText("perDay"), 0),
            new ListViewItem<int>(Program.SettingService.GetConsoleText("perWeek"), 1),
            new ListViewExtraItem<int, Controller>(Program.SettingService.GetConsoleText("backToHome"), () => new ShowToursController()),
        });

        Controller? otherController;
        int? type = typeQuestion.ShowAndGetResult<Controller>(out otherController);
        NextController = otherController; // Only set if an extra option is selected

        if (otherController == null)
            NextController = type == 0 ? new ManagerDaysOverview() : new ManagerWeeksOverview();

    }

}
