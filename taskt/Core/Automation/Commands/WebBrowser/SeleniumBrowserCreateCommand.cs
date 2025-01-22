﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using taskt.UI.CustomControls;
using taskt.UI.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace taskt.Core.Automation.Commands
{
    [Serializable]
    [Attributes.ClassAttributes.Group("Web Browser Commands")]
    [Attributes.ClassAttributes.Description("This command allows you to create a new Selenium web browser session which enables automation for websites.")]
    [Attributes.ClassAttributes.UsesDescription("Use this command when you want to create a browser that will eventually perform web automation such as checking an internal company intranet site to retrieve data")]
    [Attributes.ClassAttributes.ImplementationDescription("This command implements Selenium to achieve automation.")]
    public class SeleniumBrowserCreateCommand : ScriptCommand
    {
        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please Enter the instance name")]
        [Attributes.PropertyAttributes.InputSpecification("Signifies a unique name that will represemt the application instance.  This unique name allows you to refer to the instance by name in future commands, ensuring that the commands you specify run against the correct application.")]
        [Attributes.PropertyAttributes.SampleUsage("**myInstance** or **seleniumInstance**")]
        [Attributes.PropertyAttributes.Remarks("**myInstance** or **seleniumInstance**")]
        [Attributes.PropertyAttributes.PropertyUIHelper(Attributes.PropertyAttributes.PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        public string v_InstanceName { get; set; }

        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Instance Tracking (after task ends)")]
        [Attributes.PropertyAttributes.PropertyUISelectionOption("Forget Instance")]
        [Attributes.PropertyAttributes.PropertyUISelectionOption("Keep Instance Alive")]
        [Attributes.PropertyAttributes.InputSpecification("Specify if taskt should remember this instance name after the script has finished executing.")]
        [Attributes.PropertyAttributes.SampleUsage("Select **Forget Instance** to forget the instance or **Keep Instance Alive** to allow subsequent tasks to call the instance by name.")]
        [Attributes.PropertyAttributes.Remarks("Calling the **Close Browser** command or ending the browser session will end the instance.  This command only works during the lifetime of the application.  If the application is closed, the references will be forgetten automatically.")]
        public string v_InstanceTracking { get; set; }

        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please Select a Window State")]
        [Attributes.PropertyAttributes.PropertyUISelectionOption("Normal")]
        [Attributes.PropertyAttributes.PropertyUISelectionOption("Maximize")]
        [Attributes.PropertyAttributes.InputSpecification("Select the window state that the browser should start up with.")]
        [Attributes.PropertyAttributes.SampleUsage("Select **Normal** to start the browser in normal mode or **Maximize** to start the browser in maximized mode.")]
        [Attributes.PropertyAttributes.Remarks("")]
        public string v_BrowserWindowOption { get; set; }

        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please specify Selenium command line options (optional)")]
        [Attributes.PropertyAttributes.InputSpecification("Select optional options to be passed to the Selenium command.")]
        [Attributes.PropertyAttributes.SampleUsage("user-data-dir=c:\\users\\public\\SeleniumTasktProfile")]
        [Attributes.PropertyAttributes.Remarks("")]
        public string v_SeleniumOptions { get; set; }

        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please Select a Browser Engine Type")]
        [Attributes.PropertyAttributes.PropertyUISelectionOption("Chrome")]
        [Attributes.PropertyAttributes.PropertyUISelectionOption("Edge")]   // by ZHC, 112/7/7
        [Attributes.PropertyAttributes.PropertyUISelectionOption("IE")]
        [Attributes.PropertyAttributes.InputSpecification("Select the window state that the browser should start up with.")]
        [Attributes.PropertyAttributes.SampleUsage("Select **Normal** to start the browser in normal mode or **Maximize** to start the browser in maximized mode.")]
        [Attributes.PropertyAttributes.Remarks("")]
        public string v_EngineType { get; set; }

        // by ZHC, 114/1/21, for debugging port start
        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please specify browser debugging port like 9222 (optional)")]
        [Attributes.PropertyAttributes.InputSpecification("Input optional options to be passed to the Selenium option.")]
        [Attributes.PropertyAttributes.SampleUsage("9222")]
        [Attributes.PropertyAttributes.Remarks("")]
        public string v_DebuggingOptions { get; set; }
        // by ZHC, 114/1/21, for debugging port end

        public SeleniumBrowserCreateCommand()
        {
            this.CommandName = "SeleniumBrowserCreateCommand";
            this.SelectionName = "Create Browser";
            this.v_InstanceName = "default";
            this.CommandEnabled = true;
            this.CustomRendering = true;
            this.v_EngineType = "Chrome";
            this.v_DebuggingOptions = "";   // by ZHC, 114/1/21, for debugging port
        }

        public override void RunCommand(object sender)
        {
            var engine = (Core.Automation.Engine.AutomationEngineInstance)sender;
            var driverPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "Resources");
            var seleniumEngine = v_EngineType.ConvertToUserVariable(sender);
            var instanceName = v_InstanceName.ConvertToUserVariable(sender);

            OpenQA.Selenium.DriverService driverService;
            OpenQA.Selenium.IWebDriver webDriver;

            if (seleniumEngine == "Chrome")
            {
                OpenQA.Selenium.Chrome.ChromeOptions options = new OpenQA.Selenium.Chrome.ChromeOptions();

                if (!string.IsNullOrEmpty(v_SeleniumOptions))
                {
                    var convertedOptions = v_SeleniumOptions.ConvertToUserVariable(sender);
                    options.AddArguments(convertedOptions);
                }
                
                // by ZHC, 114/1/21, for debugging port start
                if (!string.IsNullOrEmpty(v_DebuggingOptions))
                {
                    var convertedDebuggingPort = v_DebuggingOptions.ConvertToUserVariable(sender);
                    options.DebuggerAddress = "localhost:"+convertedDebuggingPort;
                }
                // by ZHC, 114/1/21, for debugging port end

                driverService = OpenQA.Selenium.Chrome.ChromeDriverService.CreateDefaultService(driverPath);
                webDriver = new OpenQA.Selenium.Chrome.ChromeDriver((OpenQA.Selenium.Chrome.ChromeDriverService)driverService, options);
            }
            else if (seleniumEngine == "Edge")  // by ZHC, 112/7/7
            {
                OpenQA.Selenium.Edge.EdgeOptions options = new OpenQA.Selenium.Edge.EdgeOptions();

                if (!string.IsNullOrEmpty(v_SeleniumOptions))   // by ZHC, 113/9/4
                {
                    var convertedOptions = v_SeleniumOptions.ConvertToUserVariable(sender);
                    options.AddArguments(convertedOptions);
                }

                // by ZHC, 114/1/21, for debugging port start
                if (!string.IsNullOrEmpty(v_DebuggingOptions))
                {
                    var convertedDebuggingPort = v_DebuggingOptions.ConvertToUserVariable(sender);
                    options.DebuggerAddress = "localhost:" + convertedDebuggingPort;
                }
                // by ZHC, 114/1/21, for debugging port end

                driverService = OpenQA.Selenium.Edge.EdgeDriverService.CreateDefaultService(driverPath, "msedgedriver.exe");
                webDriver = new OpenQA.Selenium.Edge.EdgeDriver((OpenQA.Selenium.Edge.EdgeDriverService)driverService, options);
            }
            else
            {
                var ieOptions = new OpenQA.Selenium.IE.InternetExplorerOptions();
                ieOptions.AttachToEdgeChrome = true;    // by ZHC
                //ieOptions.EnsureCleanSession = true;    // by ZHC, 112/7/7 for Dragon.
                driverService = OpenQA.Selenium.IE.InternetExplorerDriverService.CreateDefaultService(driverPath);
                webDriver = new OpenQA.Selenium.IE.InternetExplorerDriver((OpenQA.Selenium.IE.InternetExplorerDriverService)driverService, ieOptions);
            }


            //add app instance
            engine.AddAppInstance(instanceName, webDriver);


            //handle app instance tracking
            if (v_InstanceTracking == "Keep Instance Alive")
            {
                GlobalAppInstances.AddInstance(instanceName, webDriver);
            }

            //handle window type on startup - https://github.com/saucepleez/taskt/issues/22
            switch (v_BrowserWindowOption)
            {
                case "Maximize":
                    webDriver.Manage().Window.Maximize();
                    break;
                case "Normal":
                case "":
                default:
                    break;
            }





        }
        public override List<Control> Render(frmCommandEditor editor)
        {
            base.Render(editor);


            RenderedControls.AddRange(CommandControls.CreateDefaultInputGroupFor("v_InstanceName", this, editor));
            RenderedControls.AddRange(CommandControls.CreateDefaultDropdownGroupFor("v_EngineType", this, editor));
            RenderedControls.AddRange(CommandControls.CreateDefaultDropdownGroupFor("v_InstanceTracking", this, editor));
            RenderedControls.AddRange(CommandControls.CreateDefaultDropdownGroupFor("v_BrowserWindowOption", this, editor));
            RenderedControls.AddRange(CommandControls.CreateDefaultInputGroupFor("v_SeleniumOptions", this, editor));
            RenderedControls.AddRange(CommandControls.CreateDefaultInputGroupFor("v_DebuggingOptions", this, editor)); //by ZHC, 114/1/21, for debugging port

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return "Create " + v_EngineType + " Browser - [Instance Name: '" + v_InstanceName + "', Instance Tracking: " + v_InstanceTracking + "]";
        }
    }
}