﻿using Volo.Abp.Settings;

namespace AricCar.Settings;

public class AricCarSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AricCarSettings.MySetting1));
    }
}
