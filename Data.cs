using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ogame
{
    enum Type_Building { Mine_Metal, Mine_Crystal, Mine_Deiterium, Storage_Metal, Storage_Crystal, Storage_Deiterium, Shelder_Metal, Shelder_Crystal, Shelder_Deiterium, Sun_Station, Termo_Station, Sun_Satellite, Factory, Yard, Lab, Aliens_Storage, Missle, Nano_Factory, Terraformer };
    enum Type_Research { Energy, Laser, Ion, Gyper_tech, Plasma, Jet_drive, Ion_drive, Gyper_drive, Spy, Computer_tech, Astrophysic, Galaxy_Research_network, Gravity_tech, Weapon_tech, Shield_tech, Armor_tech };
    public class Data
    {
        public const int w8Ajax = 300;
        public struct address : IComparable 
        {
            int galaxy, system, planet;
            public address(int g, int s, int p)
            {
                galaxy = g;
                system = s;
                planet = p;
            }
            public address(string _adr)
            {
                galaxy = 0;
                system = 0;
                planet = 0;
            }
            public int CompareTo(object obj)
            {
                address other = (address)(obj);
                if (galaxy != other.galaxy)
                    return this.galaxy.CompareTo(other.galaxy);
                if (system != other.system)
                    return this.system.CompareTo(other.system);

                return this.planet.CompareTo(other.planet);
            }
            
        }
        public struct resourses
        {
            public int metal, crystal, deiterium, energy;
            public resourses(int m, int c, int d=0, int e=0)
            {
                metal = m;
                crystal = c;
                deiterium = d;
                energy = e;
            }
        }
    }
    class Technology
    {
        int[] _lvl = new int[16];
        public Technology(Framework.driverObj obj)
        {
            _lvl=obj.GetLvl_Techs();
        }
        public int getLvl(Type_Research x)
        {
            return _lvl[(int)x];
        }
        public void Lvl_up(Type_Research x)
        {
            _lvl[(int)x]++;
        }
        public TimeSpan get_time_for_up(Type_Research TR, Planet _pl)
        {
            Data.resourses _res = get_resourses_for_up(TR);
   
           return TimeSpan.FromHours((_res.metal+_res.crystal) / (1000*(1+_pl.get_lvl_of_building(Type_Building.Lab))));
        }
        public Data.resourses get_resourses_for_up(Type_Research TR)
        {
            switch (TR)
            {
                case Type_Research.Energy:
                    return new Data.resourses(0, (int)(800 * Math.Pow(2, getLvl(TR))), (int)(400 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Laser:
                    return new Data.resourses((int)(000 * Math.Pow(2, getLvl(TR))), (int)(100 * Math.Pow(2, getLvl(TR))), 0);
                case Type_Research.Ion:
                    return new Data.resourses((int)(1000 * Math.Pow(2, getLvl(TR))), (int)(300 * Math.Pow(2, getLvl(TR))), (int)(100 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Gyper_tech:
                    return new Data.resourses(0, (int)(4000 * Math.Pow(2, getLvl(TR))), (int)(2000 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Plasma:
                    return new Data.resourses((int)(2000 * Math.Pow(2, getLvl(TR))), (int)(4000 * Math.Pow(2, getLvl(TR))), (int)(1000 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Jet_drive:
                    return new Data.resourses((int)(400 * Math.Pow(2, getLvl(TR))), 0, (int)(600 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Ion_drive:
                    return new Data.resourses((int)(2000 * Math.Pow(2, getLvl(TR))), (int)(4000 * Math.Pow(2, getLvl(TR))), (int)(600 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Gyper_drive:
                    return new Data.resourses((int)(10000 * Math.Pow(2, getLvl(TR))), (int)(20000 * Math.Pow(2, getLvl(TR))), (int)(6000 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Spy:
                    return new Data.resourses((int)(200 * Math.Pow(2, getLvl(TR))), (int)(1000 * Math.Pow(2, getLvl(TR))), (int)(200 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Computer_tech:
                    return new Data.resourses(0, (int)(400 * Math.Pow(2, getLvl(TR))), (int)(600 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Astrophysic:
                    return new Data.resourses((int)(4000 * Math.Pow(2, getLvl(TR))), (int)(8000 * Math.Pow(2, getLvl(TR))), (int)(4000 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Galaxy_Research_network:
                    return new Data.resourses((int)(240000 * Math.Pow(2, getLvl(TR))), (int)(400000 * Math.Pow(2, getLvl(TR))), (int)(160000 * Math.Pow(2, getLvl(TR))));
                case Type_Research.Gravity_tech:
                    return new Data.resourses(0, 0, 0, (int)(100000*Math.Pow(3,getLvl(TR))));
                case Type_Research.Weapon_tech:
                    return new Data.resourses((int)(800 * Math.Pow(2, getLvl(TR))), (int)(200 * Math.Pow(2, getLvl(TR))), 0);
                case Type_Research.Shield_tech:
                    return new Data.resourses((int)(200 * Math.Pow(2, getLvl(TR))), (int)(600 * Math.Pow(2, getLvl(TR))), 0);
                case Type_Research.Armor_tech:
                    return new Data.resourses((int)(1000 * Math.Pow(2, getLvl(TR))), 0, 0);
                default:
                    throw new NoSuchTechnology();
            }
        }
    }
    class NoSuchTechnology : SystemException
    { }
}
