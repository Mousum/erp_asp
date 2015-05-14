﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.Services.OrgSettings
{
   public interface ITaxSetting
   {
       bool AddTaxSetting(TaxSetting taxSetting);
       bool UpdateTaxSetting(TaxSetting taxSetting);
       TaxSetting GeTaxSetting(int taxSettingId);
       List<TaxSetting> GeTaxSettingList();
   }
}