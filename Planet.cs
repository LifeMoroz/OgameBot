using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame
{
    class Planet
    {
        Empire myEmpire;
        int _max_temp, _min_temp, freespace;
        Data.address _address;
        public Data.address address
        {
            private set { }
            get { return _address; }
        }
        Data.resourses _resourses;
        Building[] _Buildings = new Building[19];
        #region Описание внутреннего класса
        class Building
        {
            Planet _planet;
            private int _lvl; //для солнечного спутника выступает количеством
            public int lvl
            {
                private set { }
                get
                {
                    return _lvl;
                }
            }
            private Building() { }
            public Type_Building _type;
            public Building(Planet planet, Type_Building type, int lvl)
            {
                _planet = planet;
                _type = type;
                _lvl = lvl;
            }
            public int get_production()
            {
                switch (_type)
                {
                    case Type_Building.Mine_Metal:
                        return (int)(30 * _lvl * Math.Pow(1.1, _lvl));

                    case Type_Building.Mine_Crystal:
                        return (int)(20 * _lvl * Math.Pow(1.1, _lvl));

                    case Type_Building.Mine_Deiterium:
                        return (int)(10 * _lvl * Math.Pow(1.1, _lvl));

                    case Type_Building.Storage_Metal:
                    case Type_Building.Storage_Crystal:
                    case Type_Building.Storage_Deiterium:
                        return ((int)(2.5 * (Math.Pow(Math.E, 20 * _lvl / 33)))) * 5000;

                    case Type_Building.Shelder_Metal:
                    case Type_Building.Shelder_Crystal:
                        return (int)(600 * _lvl * Math.Pow(1.1, _lvl)) * 2;

                    case Type_Building.Shelder_Deiterium:
                        return (int)(400 * _lvl * Math.Pow(1.1, _lvl) * (1.44 - 0.004 * _planet._max_temp)) * 2;

                    case Type_Building.Sun_Station:
                        return (int)(20 * _lvl * Math.Pow(1.1, _lvl));

                    case Type_Building.Termo_Station:
                        return (int)(10 * _lvl * Math.Pow(1.05 + 0.01 * _planet.myEmpire.Techs.getLvl(Type_Research.Energy), _lvl));

                    case Type_Building.Sun_Satellite:
                        return (((_planet._max_temp + _planet._min_temp) / 2 + 160) / 6 < 50) ? (int)(((_planet._max_temp + _planet._min_temp) / 2 + 160) / 6) : 50;

                    default:
                        throw new NoSuchBuilding("error in Building.get_production(). i isn't present");
                }
            }
            public Data.resourses get_res_for_up()
            {
                switch (_type)
                {
                    /*   Рудник по добыче металла: 60*(1-1,5^уровень)/(-0,5) металла 15*(1-1,5^уровень)/(-0,5) кристалла
                         Рудник по добыче кристалла: 48*(1-1,6^уровень)/(-0,6) металла и 24*(1-1,6^уровень)/(-0,6) кристалла
                         Синтезатор дейтерия: 225*(1-1,5^уровень)/(-0,5) металла и 75*(1-1,5^уровень)/(-0,5) кристалла
                         Солнечная электростанция: 75*(1-1,5^уровень)/(-0,5) металла и 30*(1-1,5^уровень)/(-0,5) кристалла
                         Термоядерная электростанция: 900*(1-1,8^уровень)/(-0,8) металла и 360*(1-1,8^уровень)/(-0,8) кристалла и 180*(1-1,8^уровень)/(-0,8) дейтерия

                         Все остальные здания - "стоимость 1-го уровня"*((2^уровень)-1)
                     */
                    case Type_Building.Mine_Metal:
                        return new Data.resourses((int)(60 * (1 - Math.Pow(1.5, _lvl + 1)) / (-0.5)), (int)(15 * (1 - Math.Pow(1.5, _lvl + 1)) / (-0.5)), 0, (int)(10 * (_lvl + 1) * Math.Pow(1.1, _lvl + 1)));

                    case Type_Building.Mine_Crystal:
                        return new Data.resourses((int)(48 * (1 - Math.Pow(1.6, _lvl + 1)) / (-0.6)), (int)(24 * (1 - Math.Pow(1.6, _lvl + 1)) / (-0.6)), 0, (int)(10 * (_lvl + 1) * Math.Pow(1.1, _lvl + 1)));

                    case Type_Building.Mine_Deiterium:
                        return new Data.resourses((int)(225 * (1 - Math.Pow(1.5, _lvl + 1)) / (-0.6)), (int)(75 * (1 - Math.Pow(1.5, _lvl + 1)) / (-0.6)), 0, (int)(20 * (_lvl + 1) * Math.Pow(1.1, _lvl + 1)));

                    case Type_Building.Storage_Metal:
                        return new Data.resourses((int)(1000 * (Math.Pow(2, _lvl))), 0, 0);

                    case Type_Building.Storage_Crystal:
                        return new Data.resourses((int)(1000 * (Math.Pow(2, _lvl))), (int)(500 * (Math.Pow(2, _lvl))), 0);

                    case Type_Building.Storage_Deiterium:
                        return new Data.resourses((int)(1000 * (Math.Pow(2, _lvl))), (int)(1000 * (Math.Pow(2, _lvl))), 0);

                    case Type_Building.Shelder_Metal:
                        return new Data.resourses((int)(2645 * (Math.Pow(2, _lvl))), 0, 0);

                    case Type_Building.Shelder_Crystal:
                        return new Data.resourses((int)(2645 * (Math.Pow(2, _lvl))), (int)(1322 * (Math.Pow(2, _lvl))), 0);

                    case Type_Building.Shelder_Deiterium:
                        return new Data.resourses((int)(2645 * (Math.Pow(2, _lvl))), (int)(2645 * (Math.Pow(2, _lvl))), 0);

                    case Type_Building.Sun_Station:
                        return new Data.resourses((int)(75 * (1 - Math.Pow(1.5, _lvl + 1)) / (-0.5)), (int)(30 * (1 - Math.Pow(1.5, _lvl + 1)) / (-0.5)), 0);

                    case Type_Building.Termo_Station:
                        return new Data.resourses((int)(900 * (1 - Math.Pow(1.8, _lvl + 1)) / (-0.8)), (int)(360 * (1 - Math.Pow(1.8, _lvl + 1)) / (-0.8)), (int)(180 * (1 - Math.Pow(1.8, _lvl + 1)) / (-0.8)));

                    case Type_Building.Sun_Satellite:
                        return new Data.resourses(0, 2000, 500);

                    default:
                        throw new NoSuchBuilding("error in Building.get_res_for_up(). i isn't present");
                }
            }
            public TimeSpan get_time_for_up()
            {
                Data.resourses res=this.get_res_for_up();
                return TimeSpan.FromHours(((res.crystal+res.metal) / 2500) * (1 / (_planet.get_lvl_of_building(Type_Building.Factory)+1)) * Math.Pow(0.5,(_planet.get_lvl_of_building(Type_Building.Nano_Factory))));
            }
            public void get_lvl_from_server()
            {
                int[] arr = _planet.myEmpire.netbrowser.GetLvL_Resourses();
                for (int i = 0; i < arr.Length; i++)
                    _planet._Buildings[i]._lvl = arr[i];

                arr = _planet.myEmpire.netbrowser.GetLvL_Factories();
                for (int i = 0; i < arr.Length; i++)
                    _planet._Buildings[i + 12]._lvl = arr[i];
            }
        }
        class NoSuchBuilding : SystemException
        {
            public NoSuchBuilding(string message)
                : base(message)
            { }
            public NoSuchBuilding()
                : base()
            { }
            public NoSuchBuilding(string mes, Exception e)
                : base(mes, e)
            { }
        }
        #endregion
        public Planet(Empire _empire)
        {
            myEmpire = _empire;
            _resourses = new Data.resourses(500, 500, 0);
            _address = myEmpire.netbrowser.get_address();
            int[] temp = myEmpire.netbrowser.get_temperature();
            _max_temp = temp[1];
            _min_temp = temp[0];
            freespace = myEmpire.netbrowser.get_freespace();
            for (int i = 0; i < 19; i++)
            {
                _Buildings[i] = new Building(this, (Type_Building)(i), 0);
            }
        }
        public int get_lvl_of_building(Type_Building _type)
        {
            return this._Buildings[(int)_type].lvl;
        }
    }
}
