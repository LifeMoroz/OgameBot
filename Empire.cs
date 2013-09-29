using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame
{
    class Empire
    {
        private Framework.driverObj _netbrowser;
        public Framework.driverObj netbrowser
        {
            get
            {
                return _netbrowser;
            }
            private set{}
        }

        private Technology _Techs;
        public Technology Techs
        {
            private set { }
            get
            {
                return _Techs;
            }
        }

        private Planet[] planets;
        public Empire()
        {
            _netbrowser = new Framework.driverObj();
            _netbrowser.SetupTest();
            try
            {
                _netbrowser.Login("Lifemoroz", "Olenegorsk8", "Pegasus");
                planets = new Planet[1];
                planets[0] = new Planet(this);
                _Techs = new Technology(_netbrowser);
            }
            catch (SystemException x)
            {
                System.Console.WriteLine(x.Message + "\n" + x.TargetSite);
            }
        }
        private Planet get_Planet(Data.address adr)
        {
            foreach (Planet x in planets)
                if (x.address.CompareTo(adr)==0)
                    return x;
            return null;
        }
        public bool CloseDriver()
        {
            try
            {
                _netbrowser.CloseDriver();
                _netbrowser = null;
                return true;
            }
            catch (System.Exception x)
            {
                Console.WriteLine(x.Message + '\n' + x.TargetSite);
                return false;
            }
        }
    }
}
