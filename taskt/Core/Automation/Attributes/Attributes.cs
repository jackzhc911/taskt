﻿//Copyright (c) 2019 Jason Bayldon
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskt.Core.Automation.Attributes.ClassAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class Group : System.Attribute
    {
        public string groupName;
        public Group(string name)
        {
            this.groupName = name;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class Description : System.Attribute
    {
        public string commandFunctionalDescription;
        public Description(string desc)
        {
            this.commandFunctionalDescription = desc;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ImplementationDescription : System.Attribute
    {
        public string commandImplementationDescription;
        public ImplementationDescription(string desc)
        {
            this.commandImplementationDescription = desc;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class UsesDescription : System.Attribute
    {
        public string usesDescription;
        public UsesDescription(string desc)
        {
            this.usesDescription = desc;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class SubGruop : System.Attribute
    {
        public string subGruopName = "";
        public SubGruop(string group)
        {
            this.subGruopName = group;
        }
    }
}

namespace taskt.Core.Automation.Attributes.PropertyAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class InputSpecification : System.Attribute
    {
        public string inputSpecification;
        public InputSpecification(string desc)
        {
            this.inputSpecification = desc;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class SampleUsage : System.Attribute
    {
        public string sampleUsage;
        public SampleUsage(string desc)
        {
            this.sampleUsage = desc;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Remarks : System.Attribute
    {
        public string remarks;
        public Remarks(string desc)
        {
            this.remarks = desc;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class PropertyDescription : System.Attribute
    {
        public string propertyDescription;
        public PropertyDescription(string description)
        {
            this.propertyDescription = description;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class PropertyUIHelper : System.Attribute
    {
        public UIAdditionalHelperType additionalHelper;
        public PropertyUIHelper(UIAdditionalHelperType helperType)
        {
            this.additionalHelper = helperType;
        }
        public enum UIAdditionalHelperType
        {
            ShowVariableHelper,
            ShowFileSelectionHelper,
            ShowFolderSelectionHelper,
            ShowImageRecogitionHelper,
            ShowCodeBuilder,
            ShowMouseCaptureHelper,
            ShowElementRecorder,
            GenerateDLLParameters,
            ShowDLLExplorer,
            AddInputParameter,
            ShowHTMLBuilder,
            ShowIfBuilder,
            ShowLoopBuilder
        }
    }
    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class PropertyUISelectionOption : System.Attribute
    {
        public string uiOption;
        public PropertyUISelectionOption(string description)
        {
            this.uiOption = description;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class PropertySelectionChangeEvent : System.Attribute
    {
        public string uiOption;
        public PropertySelectionChangeEvent(string description)
        {
            this.uiOption = description;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyIsOptional : System.Attribute
    {
        public bool isOptional = false;
        public PropertyIsOptional(bool opt)
        {
            this.isOptional = opt;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyIsWindowNamesList : System.Attribute
    {
        public bool isWindowNamesList = false;
        public bool allowCurrentWindow = true;
        public bool allowAllWindows = false;
        public bool allowDesktop = false;
        public PropertyIsWindowNamesList(bool isWindowNameList)
        {
            this.isWindowNamesList = isWindowNameList;
        }
        public PropertyIsWindowNamesList(bool isWindowNameList, bool allowCurrent)
        {
            this.isWindowNamesList = isWindowNameList;
            this.allowCurrentWindow = allowCurrent;
        }
        public PropertyIsWindowNamesList(bool isWindowNameList, bool allowCurrent, bool allowAll)
        {
            this.isWindowNamesList = isWindowNameList;
            this.allowCurrentWindow = allowCurrent;
            this.allowAllWindows = allowAll;
        }
        public PropertyIsWindowNamesList(bool isWindowNameList, bool allowCurrent, bool allowAll, bool allowDesktop)
        {
            this.isWindowNamesList = isWindowNameList;
            this.allowCurrentWindow = allowCurrent;
            this.allowAllWindows = allowAll;
            this.allowDesktop = allowDesktop;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyIsVariablesList : System.Attribute
    {
        public bool isVariablesList = false;
        public PropertyIsVariablesList(bool opt)
        {
            this.isVariablesList = opt;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyRecommendedUIControl : System.Attribute
    {
        public RecommendeUIControlType recommendedControl = RecommendeUIControlType.TextBox;
        public PropertyRecommendedUIControl(RecommendeUIControlType ctl)
        {
            this.recommendedControl = ctl;
        }
        public enum RecommendeUIControlType
        {
            TextBox,
            ComboBox,
            DataGridView,
            MultiLineTextBox,
            CheckBox,
            RadioButton,
            TextLink,
            Label
        }
    }
    [System.AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyShowSampleUsageInDescription : System.Attribute
    {
        public bool showSampleUsage = false;
        public PropertyShowSampleUsageInDescription(bool opt)
        {
            this.showSampleUsage = opt;
        }
    }
}