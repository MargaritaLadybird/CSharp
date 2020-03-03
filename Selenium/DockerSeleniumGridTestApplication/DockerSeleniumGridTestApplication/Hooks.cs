﻿using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace DockerSeleniumGridTestApplication
{
    public enum BrowserType
    {
        Chrome,
        FireFox,
        IE
    }
    
    [TestFixture]
    public class Hooks : Base
    {
        private BrowserType _browserType;
        public Hooks(BrowserType browser)
        {
            _browserType = browser;
        }

        [SetUp]
        public void InitializeTest()
        {
            ChooseDriverInstance(_browserType);
        }
        private void ChooseDriverInstance(BrowserType browserType)
        {
            if (browserType == BrowserType.Chrome)
            {
                var caps = new ChromeOptions();
                Driver = new RemoteWebDriver(new Uri("http://localhost:4545/wd/hub"), caps);
            }
            else if (browserType == BrowserType.FireFox)
            {
                var caps = new FirefoxOptions();
                Driver = new RemoteWebDriver(new Uri("http://localhost:4545/wd/hub"), caps);
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
