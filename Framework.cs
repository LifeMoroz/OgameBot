using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace Framework
{
    [TestFixture]
    public class driverObj
    {
        
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://ogame.ru";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void CloseDriver()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        public void Login(string UserName, string Password, string NameOfGalaxy)
        {
            //переходим на сайт
            driver.Navigate().GoToUrl(baseURL);

            //ищем и тыкаем кнопочку логин
            driver.FindElement(By.Id("loginBtn")).Click();
            //выбираем мир
            SelectElement selectElem = new SelectElement(driver.FindElement(By.Id("serverLogin")));
            selectElem.SelectByText(NameOfGalaxy);
            //вводим логин
            driver.FindElement(By.Id("usernameLogin")).Click();
            IWebElement query = driver.FindElement(By.Id("usernameLogin"));
            query.SendKeys(UserName);
            //вводим пароль
            driver.FindElement(By.Id("passwordLogin")).Click();
            query = driver.FindElement(By.Id("passwordLogin"));
            query.SendKeys(Password);

            //тыкаем "Логин"
            driver.FindElement(By.Id("loginSubmit")).Click();

            driver.FindElement(By.CssSelector(".selected")).Click();
            baseURL = driver.Url.Substring(0, 22) + "/";
        }
        public int[] GetLvL_Resourses()
        {
            int[] _lvl=new int[12];
            driver.Navigate().GoToUrl(baseURL + "game/index.php?page=resources");
                try
                {
                    _lvl[0] = int.Parse(driver.FindElement(By.XPath("//li/div/div/a/span")).Text.Substring(driver.FindElement(By.XPath("//li/div/div/a/span")).Text.Length - 2));
                    for (int count = 2; count < 12; count++)
                        _lvl[count - 1] = int.Parse(driver.FindElement(By.XPath("(//a[@id='details']/span)[" + count + "]")).Text.Substring(driver.FindElement(By.XPath("(//a[@id='details']/span)[" + count + "]")).Text.Length - 2));
                    _lvl[11] = int.Parse(driver.FindElement(By.XPath("(//a[@id='details']/span/span)[12]")).Text.Substring(driver.FindElement(By.XPath("(//a[@id='details']/span/span)[12]")).Text.Length - 2));
                    return _lvl;
                }
                catch (System.Exception)
                {throw;}
        }
        public int[] GetLvL_Factories()
        {
            int Factory, Yard, Research_station, Alians_storage, Missle_silos, Nanites_factory, Terraformer;
            driver.Navigate().GoToUrl(baseURL + "game/index.php?page=station");
            String temp;
            int i = 0;
            while (true)
            {
                try
                {
                    temp = driver.FindElement(By.XPath("//a[@id='details14']/span")).Text;
                    temp = temp.Substring(temp.Length - 2);
                    Factory = int.Parse(temp);
                    temp = driver.FindElement(By.XPath("//a[@id='details21']/span")).Text;
                    temp = temp.Substring(temp.Length - 2);
                    Yard = int.Parse(temp);
                    temp = driver.FindElement(By.XPath("//a[@id='details31']/span")).Text;
                    temp = temp.Substring(temp.Length - 2);
                    Research_station = int.Parse(temp);
                    temp = driver.FindElement(By.XPath("//a[@id='details34']/span")).Text;
                    temp = temp.Substring(temp.Length - 2);
                    Alians_storage = int.Parse(temp);
                    temp = driver.FindElement(By.XPath("//a[@id='details44']/span")).Text;
                    temp = temp.Substring(temp.Length - 2);
                    Missle_silos = int.Parse(temp);
                    temp = driver.FindElement(By.XPath("//a[@id='details15']/span")).Text;
                    temp = temp.Substring(temp.Length - 2);
                    Nanites_factory = int.Parse(temp);
                    temp = driver.FindElement(By.XPath("//a[@id='details33']/span/span")).Text;
                    temp = temp.Substring(temp.Length - 2);
                    Terraformer = int.Parse(temp);
                    return new int[] { Factory, Yard, Research_station, Alians_storage, Missle_silos, Nanites_factory, Terraformer };
                }
                catch (NoSuchElementException)
                {
                    i++;
                    if (i > 50)
                        throw;
                }

            }
            /*
            temp = temp.Substring(temp.Length - 2);
            Factory = int.Parse(temp);

            temp = driver.FindElement(By.CssSelector("#details21 > span:nth-child(1) > span:nth-child(1)")).Text;
            temp = temp.Substring(temp.Length - 2);
            Yard = int.Parse(temp);

            temp = driver.FindElement(By.CssSelector("#details31 > span:nth-child(1)")).Text;
            temp = temp.Substring(temp.Length - 2);
            Research_station = int.Parse(temp);

            temp = driver.FindElement(By.CssSelector("#details34 > span:nth-child(1)")).Text;
            temp = temp.Substring(temp.Length - 2);
            Alians_storage = int.Parse(temp);

            temp = driver.FindElement(By.CssSelector("#details44 > span:nth-child(1) > span:nth-child(1)")).Text;
            temp = temp.Substring(temp.Length - 2);
            Missle_silos = int.Parse(temp);

            temp = driver.FindElement(By.CssSelector("#details15 > span:nth-child(1) > span:nth-child(1)")).Text;
            temp = temp.Substring(temp.Length - 2);
            Nanites_factory = int.Parse(temp);

            temp = driver.FindElement(By.CssSelector("#details33 > span:nth-child(1) > span:nth-child(1)")).Text;
            temp = temp.Substring(temp.Length - 2);
            Terraformer = int.Parse(temp);
             */
        }
        public int[] GetLvl_Techs()
        {
            int [] _lvls=new int[16];
            driver.Navigate().GoToUrl(baseURL + "game/index.php?page=research");
            _lvls[0]= int.Parse(driver.FindElement(By.XPath("//a[@id='details113']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details113']/span")).Text.Length - 2));
            _lvls[1]= int.Parse(driver.FindElement(By.XPath("//a[@id='details120']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details120']/span")).Text.Length - 2));
            _lvls[2]= int.Parse(driver.FindElement(By.XPath("//a[@id='details121']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details121']/span")).Text.Length - 2));
            _lvls[3]= int.Parse(driver.FindElement(By.XPath("//a[@id='details114']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details114']/span")).Text.Length - 2));
            _lvls[4]= int.Parse(driver.FindElement(By.XPath("//a[@id='details122']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details122']/span")).Text.Length - 2));
            _lvls[5]= int.Parse(driver.FindElement(By.XPath("//a[@id='details115']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details115']/span")).Text.Length - 2));
            _lvls[6]= int.Parse(driver.FindElement(By.XPath("//a[@id='details117']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details117']/span")).Text.Length - 2));
            _lvls[7]= int.Parse(driver.FindElement(By.XPath("//a[@id='details118']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details118']/span")).Text.Length - 2));
            _lvls[8]= int.Parse(driver.FindElement(By.XPath("//a[@id='details106']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details106']/span")).Text.Length - 2));
            _lvls[9]= int.Parse(driver.FindElement(By.XPath("//a[@id='details108']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details108']/span")).Text.Length - 2));
            _lvls[10]= int.Parse(driver.FindElement(By.XPath("//a[@id='details124']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details124']/span")).Text.Length - 2));
            _lvls[11]= int.Parse(driver.FindElement(By.XPath("//a[@id='details123']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details123']/span")).Text.Length - 2));
            _lvls[12]= int.Parse(driver.FindElement(By.XPath("//a[@id='details199']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details199']/span")).Text.Length - 2));
            _lvls[13]= int.Parse(driver.FindElement(By.XPath("//a[@id='details109']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details109']/span")).Text.Length - 2));
            _lvls[14]= int.Parse(driver.FindElement(By.XPath("//a[@id='details110']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details110']/span")).Text.Length - 2));
            _lvls[15]= int.Parse(driver.FindElement(By.XPath("//a[@id='details111']/span")).Text.Substring(driver.FindElement(By.XPath("//a[@id='details111']/span")).Text.Length - 2));
            return _lvls;
        }
        public Ogame.Data.resourses Get_Resources()
        {
            string temp;
            int metal, crystal, deit;
            temp= driver.FindElement(By.Id("resources_metal")).Text;
            metal=int.Parse(temp.Substring(temp.IndexOf(' ')+1));

            temp= driver.FindElement(By.Id("resources_crystal")).Text;
            crystal=int.Parse(temp.Substring(temp.IndexOf(' ')+1));

            temp= driver.FindElement(By.Id("resources_deuterium")).Text;
            deit=int.Parse(temp.Substring(temp.IndexOf(' ')+1));

            return new Ogame.Data.resourses(metal,crystal,deit);
        }

        private string get_data_from_ajax(By by) 
        {
            if (driver.Url != baseURL + "game/index.php?page=overview") 
                driver.Navigate().GoToUrl(baseURL + "game/index.php?page=overview");
            string temp= null;
            do
            {
                try
                {

                    temp = driver.FindElement(by).Text;
                    Thread.Sleep(Ogame.Data.w8Ajax);
                }
                catch (NoSuchElementException)
                { continue; }
            } while ((temp==null)||(!(temp.Length > 3) || temp != driver.FindElement(by).Text));
            return temp;
        }
        public Ogame.Data.address get_address()
        {
            string[] temp = get_data_from_ajax(By.Id("positionContentField")).Split('[',']',':');

            return new Ogame.Data.address(int.Parse(temp[1]),int.Parse(temp[2]),int.Parse(temp[3]));
        }
        public int[] get_temperature()
        {
            string[] temp = get_data_from_ajax(By.Id("temperatureContentField")).Split(' ', (char)(176));
           
            return new int[] { int.Parse(temp[1]), int.Parse(temp[4]) };
        }
        public int get_freespace()
        {
            string[] temp = get_data_from_ajax(By.Id("diameterContentField")).Split('(', ')', '/', ' ', (char)(176));

            return int.Parse(temp[3])-int.Parse(temp[2]);
        }
        public void IsAttackPresent() //закончена
        {
                try
                {
                    if(driver.Url.Contains("galaxy")) //в режиме галактики не доступно поле событий флота.
                        driver.Navigate().GoToUrl(baseURL+"game/index.php?page=overview");
                    if (driver.FindElement(By.Id("eventboxBlank")).Text.ToLower() != "нет передвижения флотов")
                    {
                        driver.FindElement(By.CssSelector("#eventboxFilled")).Click();
                        Thread.Sleep(500);
                        try
                        {
                            var x = driver.FindElements(By.XPath("//tr[contains(@id,'event') and td[contains(@class,'hostile')]]"));
                            foreach (var y in x)
                                Console.WriteLine(y.Text); //осталось распарсить строку и определить куда прелетает вражина и во сколько, потом выставить таймер
                        }
                        catch(OpenQA.Selenium.NoSuchElementException)
                        { }

                    }                                    
                }
                catch (NoSuchElementException)
                {
                }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
