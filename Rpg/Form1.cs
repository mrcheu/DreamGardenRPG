using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;


namespace Rpg
{



    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //--狀態列--
        public int round = 1;//回合數
        public int round2 = 1;//總回合數
        public int round3 = 1;//平均回合
        public int killNum = 0;//殺死怪物數
        public int gold = 0;//金幣數
        public int moneytotle = 0;//總獲取金幣數
        public int level = 1;//等級數
        public int Exp = 0;//等級經驗值
        public int ExpUp = 100;//等級經驗值上限 (到達才會升等)       
        //--打怪資訊--
        public int attack1 = 5;//主角基礎攻擊力
        public int attack2 = 0;//額外加強的攻擊力
        public int attack3 = 5;//合計攻擊力 (基礎+額外)
        public int attackrandom = 0;//主角攻擊力亂數用
        public int monster = 25;//怪物血量
        public int newmonster = 25;//新怪物亂數使用
        public int monstergold = 5;//怪物掉落金幣量
        public int monsterExp = 10;//怪物掉落金幣量
        public string line = "---------------------------------------------";//分行使用
        //--怪物資訊---
        public int Monsterlength = 9 - 1;//陣列從0開始 因此-1
        public int MonAryRondam = 0;//用來接陣列內亂數數值
        public string[] monsterArray = new string[] { "史萊姆", "黑木妖", "三眼章魚", "藍水靈", "綠菇菇", "藍菇菇", "小仙人掌", "緞帶肥肥", "殭屍菇菇", "刀疤土雞", "綠茶幫-小晨魔王" };
        public int 怪物攻擊力;
        public int 怪物等級=1;
        public int 怪物升等累積 = 0;
        //--升級資訊--
        public int upatkgold = 20;//初始升級攻擊力花費
        public int 雙倍攻擊持續時間;
        //--技能蓄力--
        public int skillAup = 6;//使用6次普通攻擊可使用技能
        public int skillA = 0;//累積蓄力值
        //--對話窗--
        public int plotL1 = 0;
        public int plotL2 = 0;
        public int plotL3 = 0;
        public int plotL4 = 0;
        public int 劇情進度 = 1;
        //--命名--
        public string[] 角色名稱生成 = new string[] { "中痛好酸林", "溫刀起低", "金色狂風", "無心插柳柳橙汁", "梅川依芙", "湯姆喀土司", "女乃豆頁石更", "大盜韓不助","澎湖一條根","我叫謝恩恩" ,"小櫻純真可愛","愛情摩天輪"};
        public int 角色名稱亂數;
        //--寵物資訊--
        public int 寵物攻擊力 = 0;
        public int 寵物攻速 = 0;
        public int 寵物是否有攻擊 = 0;
        public int 寵物種類=0;
        public int 買哪隻寵物 = 0;
        //--BOSS資訊--
        public int 魔王幹部血量 = 9000;
        public int 魔王幹部經驗 = 3000;
        public int 魔王幹部金幣 = 2500;
        public int 魔王血量 = 30000;



        void Strange()//生成怪物
        {
            Random Rnd = new Random();//寫入內建亂數產生class
            怪物升等累積++;
            if (怪物升等累積 == 2)//等於打死兩隻怪 怪物會升一等
            {
                怪物等級++;
                怪物升等累積 = 0;
            }
            newmonster = Rnd.Next(newmonster * 4 / 3, newmonster * 3 / 2); ;//新怪物血量增強 範圍(1.3~1.5)倍率
            monster = newmonster;//可變動的怪物血量 套入 新怪物血量
            monsterExp = Rnd.Next(monsterExp * 6 / 5, monsterExp * 7 / 4);//隨機產生下一次經驗獲取量 範圍(1.2~1.75)倍率
            monstergold = Rnd.Next(monstergold * 6 / 5, monstergold * 6 / 4);//隨機產生下一次金幣獲取量 範圍(1.2~1.5)倍率

            MonsterBar1.Maximum = newmonster;//設置怪物血量最大長度值 

            if (monster <= 0)
            {
                MonsterBar1.Value = 0;//設置當前值              
            }
            else
            {
                MonsterBar1.Value = monster;//設置當前值
            }
        }

        void BOSS1()
        {
            Random Rnd = new Random();//寫入內建亂數產生class
            怪物等級 = 12;
            newmonster = 魔王幹部血量;
            MonAryRondam = 9;//名稱變幹部
            monster = newmonster;//可變動的怪物血量 套入 新怪物血量
            monsterExp = Rnd.Next(魔王幹部經驗 * 6 / 5, 魔王幹部經驗 * 7 / 4);//隨機產生下一次經驗獲取量 範圍(1.2~1.75)倍率
            monstergold = Rnd.Next(魔王幹部金幣 * 6 / 5, 魔王幹部金幣 * 6 / 4);//隨機產生下一次金幣獲取量 範圍(1.2~1.5)倍率

            MonsterBar1.Maximum = newmonster;//設置怪物血量最大長度值        
            MonsterBar1.Value = monster;//設置當前值
        }
        void BOSS2()
        {
            Random Rnd = new Random();//寫入內建亂數產生class
            怪物等級 = 15;
            newmonster = 魔王血量;
            MonAryRondam = 10;//名稱變魔王
            monster = newmonster;//可變動的怪物血量 套入 新怪物血量
            
            MonsterBar1.Maximum = newmonster;//設置怪物血量最大長度值        
            MonsterBar1.Value = monster;//設置當前值
        }

        void atk1()//普通攻擊 倍率(1~2.3)
        {
            Random Rnd = new Random();//寫入內建亂數產生class
            if (check(ref 雙倍攻擊持續時間))
            {
                int attack4 = attack3 * 2;//雙倍攻擊
                attackrandom = Rnd.Next(attack4 * 1, attack4 * 7 / 3);//攻擊亂數區間 範圍(1~2.3)倍率
                怪物攻擊力 = Rnd.Next((killNum + 1) * 0, (killNum + 1) * 4 / 3);
                if (MonAryRondam == 10)
                    怪物攻擊力 += 15;
            }
            else
            {
                attackrandom = Rnd.Next(attack3 * 1, attack3 * 7 / 3);//攻擊亂數區間 範圍(1~2.3)倍率
                怪物攻擊力 = Rnd.Next((killNum + 1) * 0, (killNum + 1) * 4 / 3);
                if (MonAryRondam == 10)
                    怪物攻擊力 += 15;
            }

            if (monster == newmonster)//如果怪物死亡重製的第一回合
            {
                round = 0;//回合數重製                
                if (attackrandom >= attack3 * 2)
                {
                    richTextBox1.Text += textBox1.Text + " 使用 普通攻擊 爆擊!! 對怪獸造成" + attackrandom + "點傷害\n";//第一次攻擊造成傷害資訊
                    主角目前血量 = 主角目前血量 - 怪物攻擊力;
                    richTextBox1.Text += "怪獸攻擊" + textBox1.Text + "造成了" + 怪物攻擊力 + "點的傷害\n主角剩下" + 主角目前血量 + "/" + 主角總血量 + "\n";
                    寵物是否有攻擊 = petattack();
                   

                }
                else
                {
                    richTextBox1.Text += textBox1.Text + " 使用 普通攻擊 對怪獸造成" + attackrandom + "點傷害\n";//第一次攻擊造成傷害資訊
                    主角目前血量 = 主角目前血量 - 怪物攻擊力;
                    richTextBox1.Text += "怪獸攻擊" + textBox1.Text + "造成了" + 怪物攻擊力 + "點的傷害\n主角剩下" + 主角目前血量 + "/" + 主角總血量 + "\n";
                    寵物是否有攻擊 = petattack();


                }
            }
            else //普通打怪
            {
                if (attackrandom >= attack3 * 2)//觸發爆擊
                {
                    richTextBox1.Text = "LV"+怪物等級+" "+monsterArray[MonAryRondam] + " 總血量為" + newmonster + "\n" + line + "\n" + textBox1.Text + " 使用 普通攻擊 爆擊!! 對怪獸造成" + attackrandom + "點傷害\n";//攻擊造成傷害資訊
                    主角目前血量 = 主角目前血量 - 怪物攻擊力;
                    richTextBox1.Text += "怪獸攻擊" + textBox1.Text + "造成了" + 怪物攻擊力 + "點的傷害\n主角剩下" + 主角目前血量 + "/" + 主角總血量 + "\n";
                    寵物是否有攻擊 = petattack();

                }
                else
                {
                    richTextBox1.Text = "LV" + 怪物等級 + " "+monsterArray[MonAryRondam] + " 總血量為" + newmonster + "\n" + line + "\n" + textBox1.Text + " 使用 普通攻擊 對怪獸造成" + attackrandom + "點傷害\n";//攻擊造成傷害資訊
                    主角目前血量 = 主角目前血量 - 怪物攻擊力;
                    richTextBox1.Text += "怪獸攻擊" + textBox1.Text + "造成了" + 怪物攻擊力 + "點的傷害\n主角剩下" + 主角目前血量 + "/" + 主角總血量 + "\n";
                    寵物是否有攻擊 = petattack();

                }
            }
            monster = monster - attackrandom-寵物是否有攻擊;//怪物血量受到攻擊減少            
            if (monster <= 0)//怪物血量低於0，死亡
            {
                richTextBox1.Text += "怪物受到" + (attackrandom + 寵物是否有攻擊) + "點攻擊，血量歸0\n";//怪物死亡資訊
                richTextBox1.Text += "獲得金幣" + monstergold + "  獲得經驗" + monsterExp;//獲取 金幣 經驗 資訊
                Exp += monsterExp;//獲取經驗存入經驗庫                
                ExpLevel();//提升經驗值之副程式
                gold += monstergold;//獲取金幣存入金幣袋
                moneytotle += monstergold;//獲取總累積金幣   
                //--                
                round++;//回合數計算
                round2++;//平均回合數計算

                killNum++;//增加殺敵數
                //MonsterBar1.Value = 0;//設置怪物血量0
                updatedata();//重製狀態列資訊
                //MonsterBar1.Value = 0;//設置怪物血量0
                收集的靈魂數 += 1;
                label21.Text = "0/" + newmonster;
                if (收集的靈魂數 > 3)
                {
                    收集的靈魂數 = 3;
                }
                if (level < 10)//10等就不會再打開
                {
                GO.Enabled = true;
                }
                
                SkillA1.Enabled = false;
                button1.Enabled = false;
            }

            else //怪物沒死          
            {
                richTextBox1.Text += "怪物受到" + (attackrandom+寵物是否有攻擊) + "點攻擊，還剩餘" + monster + "點血量\n";//怪物剩餘血量資訊
                label21.Text = monster + "/" + newmonster;
                round++;//回合數計算
                round2++;//平均回合數計算
                updatedata();//重製狀態列資訊
            }
        }
        //--
        void atk2()//技能A攻擊 倍率(6~7) 蓄力值0/6
        {
            Random Rnd = new Random();//寫入內建亂數產生class
            attackrandom = 升級技能A加成;
            if (check(ref 雙倍攻擊持續時間))
            {
                int attack4 = attack3 * 2;
                richTextBox1.Text += "使用藥水加成攻擊";
                attackrandom += Rnd.Next(attack4 * 6, attack4 * 7);//技能攻擊亂數區間 範圍(6~7)倍率
                怪物攻擊力 = Rnd.Next((killNum + 1) * 0, (killNum + 1) * 4 / 3);

                if (MonAryRondam == 10)
                    怪物攻擊力 += 15;

            }
            else
            {
                attackrandom += Rnd.Next(attack3 * 6, attack3 * 7);//技能攻擊亂數區間 範圍(6~7)倍率
                怪物攻擊力 = Rnd.Next((killNum + 1) * 0, (killNum + 1) * 4 / 3);
                if (MonAryRondam == 10)                
                    怪物攻擊力 += 15;                
            }
            if (monster == newmonster)//如果怪物死亡重製的第一回合
            {
                round = 0;//回合數重製
                richTextBox1.Text += textBox1.Text + " 使用 蓄力斬 對怪獸造成" + attackrandom + "點傷害\n";//第一次攻擊造成傷害資訊
                主角目前血量 = 主角目前血量 - 怪物攻擊力;
                richTextBox1.Text += "怪獸攻擊" + textBox1.Text + "造成了" + 怪物攻擊力 + "點的傷害\n主角剩下" + 主角目前血量 + "/" + 主角總血量 + "\n"; ;
                寵物是否有攻擊 = petattack();

            }
            else //普通打怪
            {

                richTextBox1.Text = "LV" + 怪物等級 + " "+monsterArray[MonAryRondam] + " 總血量為" + newmonster + "\n" + line + "\n" + textBox1.Text + " 使用 蓄力斬 對怪獸造成" + attackrandom + "點傷害\n";//攻擊造成傷害資訊
                主角目前血量 = 主角目前血量 - 怪物攻擊力;
                richTextBox1.Text += "怪獸攻擊" + textBox1.Text + "造成了" + 怪物攻擊力 + "點的傷害\n主角剩下" + 主角目前血量 + "/" + 主角總血量 + "\n"; ;
                寵物是否有攻擊 = petattack();

            }

            monster = monster - attackrandom-寵物是否有攻擊;//怪物血量受到攻擊減少

            if (monster <= 0)//怪物血量低於0，死亡
            {
                richTextBox1.Text += "怪物受到" + (attackrandom+寵物是否有攻擊) + "點攻擊，血量歸0\n";//怪物死亡資訊
                richTextBox1.Text += "獲得金幣" + monstergold + "  獲得經驗" + monsterExp;//獲取 金幣 經驗 資訊
                Exp += monsterExp;//獲取經驗存入經驗庫
                ExpLevel();//提升經驗值之副程式
                gold += monstergold;//獲取金幣存入金幣袋
                moneytotle += monstergold;//獲取總累積金幣
                //--
                Strange();//生成怪物
                round++;//回合數計算
                round2++;//平均回合數計算
                killNum++;//增加殺敵數
                updatedata();//重製狀態列資訊
                MonsterBar1.Value = 0;//設置怪物血量0
                收集的靈魂數 += 1;
                if (收集的靈魂數 > 3)
                {
                    收集的靈魂數 = 3;
                }
                if (level < 10)//10等就不會再打開
                {
                    GO.Enabled = true;
                }
                SkillA1.Enabled = false;
                button1.Enabled = false;
            }
            else //怪物沒死          
            {
                richTextBox1.Text += "怪物受到" + (attackrandom+寵物是否有攻擊) + "點攻擊，還剩餘" + monster + "點血量\n";//怪物剩餘血量資訊
                label21.Text = monster + "/" + newmonster;
                round++;//回合數計算
                round2++;//平均回合數計算
                updatedata();//重製狀態列資訊
            }
        }
        //--
        void upatk1()//升級普功
        {
            if (gold >= upatkgold)//如果身上金幣>升級花費
            {
                gold -= upatkgold;//金幣扣掉花費
                attack2 += 10;//額外攻擊力增加10
               
                
                upatkgold += 10;//下一次升級所需金錢增加
                upgold1.Text = "花費 " + upatkgold + " $";

            }

            if (MonsterBar1.Value == 0)//如果升級時怪物血量為0，則進度條最後須保留0
            {
                updatedata();//更新進度條
                MonsterBar1.Value = 0;
            }
            else
            {
                updatedata();
            }
        }

        void updatedata()//各狀態更新
        {
            //---等級經驗更新
            leavlText.Text = "LV" + level;//更新等級狀態列
            level2text.Text = Exp + "/" + ExpUp;
            expBar1.Maximum = ExpUp;//設置最大長度值
            expBar1.Value = Exp;//設置當前值
            //---回合數更新
            roundText.Text = "回合數： " + round;//更新回合狀態列
            roundText2.Text = "總計回合數： " + round2;//更新總計回合狀態列
            if (killNum != 0)//殺敵數=0時不執行
            {
                round3 = round2 / killNum;
            }
            roundText3.Text = "平均回合數： " + round3;//更新平均回合狀態列  
                                                 //---怪物血量條---

            MonsterBar1.Maximum = newmonster;//設置怪物血量最大長度值 
            if (monster <= 0)
            {
                MonsterBar1.Value = 0;//設置當前值              
            }
            else
            {
                MonsterBar1.Value = monster;//設置當前值
            }

            //---狀態列更新
            goldText.Text = "" + gold;//更新金幣狀態列
            goldText2.Text = "" + gold;//更新第二金幣狀態列
            attack3 = attack1 + attack2;
            atk01.Text = "" + attack3;//更新攻擊力狀態
            monserNum.Text = "打倒怪物數量： " + killNum;
            moneytoteltext.Text = "累積總金幣： " + moneytotle;
            atkuptext.Text = "攻擊力共增加了 " + attack2;



        }


        void skillupdata()//技能進度條更新
        {
            //---技能蓄力狀態--
            skillBar1.Maximum = skillAup;//技能a最大值
            skillBar1.Value = skillA;//技能a蓄力狀況
        }

        void ExpLevel()//升等
        {
            if (Exp > ExpUp)//如果經驗大於經驗條
            {
                level++;//等級升級
                attack1 = attack1 * 6 / 5;//升級 基礎攻擊力上升1.2倍
               
                Exp -= ExpUp;//exp扣掉上限 如 110/100 -> 10/110 下一階經驗上限為110
                ExpUp = ExpUp * 3 / 2;//經驗上限*1.5成長
                expBar1.Step = Exp;
                主角總血量 = (int)(主角總血量 * 1.1);
                主角目前血量 = 主角總血量;//升級會變成滿血
                progressBar1.Maximum = 主角總血量;//設置主角血量最大長度值 
                progressBar1.Value = 主角目前血量;//設置主角血量最大長度值 
                技能B回復血量 = 技能B回復血量 + 20;//回復血量增加20
                button4.Text = "治癒術~回復" + 技能B回復血量 + "的血量";
                richTextBox1.Text += "主角升等~目前最大血量為" + 主角總血量 + "\n";

                
            }
            if (level >= 10)
                {
                GO.Enabled = false;    
                開始對話關閉按鈕();
                MessageBox.Show("請繼續進行對話");
                }
        }


        int petattack()//寵物攻擊
        {
            寵物是否有攻擊 = 0;
            
            if (寵物種類 == 1)//偵測主角攻擊力，來判定寵物攻擊力
            {
                寵物攻擊力 = (attack1 + attack2) / 2;//寵物攻擊提升
            }
            else if (寵物種類 == 2)
            {
                寵物攻擊力 = (attack1 + attack2) * 5;//寵物攻擊提升
            }


            if (寵物攻速 == 0)
            {
                寵物是否有攻擊 = 0;
            }
            else if (寵物攻速 == 1)
            {
                richTextBox1.Text += "寵物攻擊了怪獸" + "對怪獸造成了" + 寵物攻擊力 + "點傷害\n";
                寵物是否有攻擊 = 寵物攻擊力;
            }
            else if ((round+1)  % 寵物攻速 == 0)
            {
                richTextBox1.Text += "寵物攻擊了怪獸" + "對怪獸造成了" + 寵物攻擊力 + "點傷害\n";
                寵物是否有攻擊 = 寵物攻擊力;
            }
            return 寵物是否有攻擊;
        }

        private void button1_Click(object sender, EventArgs e)//普通攻擊
        {
            atk1();
            if (skillA < skillAup)//不讓他加超過
            {
                skillA++;//蓄力值+1
                skillupdata();
            }
            if (主角目前血量 <= 0)
            {
                MessageBox.Show("主角血量歸0~ GAME OVER");
                Environment.Exit(0);
            }
            progressBar1.Value = 主角目前血量;
            label20.Text = 主角目前血量 + "/" + 主角總血量;
            progressBar2.Value = 收集的靈魂數;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            upatk1();


        }//升級攻擊力

        private void button8_Click(object sender, EventArgs e)//一鍵升級
        {

            while (gold > upatkgold)//如果身上金幣>升級花費
            {
                gold -= upatkgold;//金幣扣掉花費
                attack2 += 10;//攻擊力增加10
                upatkgold += 20;
                upgold1.Text = "花費 " + upatkgold + " $";
               

            }
            updatedata();


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }//戰鬥資訊窗

        private void SkillA1_Click(object sender, EventArgs e)//技能A
        {

            if (skillA == skillAup)
            {
                atk2();
                skillA = 0;//消耗蓄力值
                skillupdata();
            }
            if (主角目前血量 <= 0)
            {
                MessageBox.Show("主角血量歸0~ GAME OVER");
                Environment.Exit(0);
            }
            progressBar1.Value = 主角目前血量;
            progressBar2.Value = 收集的靈魂數;
            label20.Text = 主角目前血量 + "/" + 主角總血量;
        }

        void 對話完畢開啟按鈕()
        {
            GO.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button9.Enabled = true;
            button11.Enabled = true;
            button14.Enabled = true;
            button15.Enabled = true;
            if (買哪隻寵物 == 1)
            {
                button17.Enabled = true;
            }
            if (買哪隻寵物 == 2)
            {
                button18.Enabled = true;
            }
            button7.Enabled = false;
        }
        void 開始對話關閉按鈕()
        {
            GO.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button9.Enabled = false;
            button11.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            button17.Enabled = false;
            button18.Enabled = false;
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {           
            if(textBox1.Text == "請輸入暱稱" || textBox1.Text == "")
            {
                MessageBox.Show("請先完成命名！！！");
            }
            else
            {
                switch (劇情進度)
                {
                    case 1://第一段劇情
                        richTextBox2.Text = plot1[plotL1];
                        if (plotL1 < plot1.Length - 1)
                        {
                            plotL1++;
                        }
                        else
                        {
                            MessageBox.Show("請使用下列按鈕\n開始你的戰鬥吧");
                            對話完畢開啟按鈕();
                            A.Stop();
                            劇情進度 = 2;
                            A = new SoundPlayer(@"mu.wav");
                            A.PlayLooping();
                        }
                        break;
                    case 2://第二段劇情
                        richTextBox2.Text = plot2[plotL2];//第二段劇情
                        if (plotL2 < plot2.Length - 1)
                        {
                            plotL2++;
                        }
                        else
                        {
                            MessageBox.Show("請使用下列按鈕\n繼續你的戰鬥吧");
                            對話完畢開啟按鈕();
                            BOSS1();
                            richTextBox1.Text = "遇到 LV " + 怪物等級 + " " + monsterArray[MonAryRondam] + " ，怪物血量為" + monster + "\n" + line + "\n";//顯示新怪物血量資訊         
                            label21.Text = monster + "/" + newmonster;
                            button1.Enabled = true;
                            SkillA1.Enabled = true;
                            GO.Enabled = false;
                            劇情進度 = 3;
                        }
                        break;
                    case 3://第二段劇情
                        richTextBox2.Text = plot3[plotL3];//第二段劇情
                        if (plotL3 < plot3.Length - 1)
                        {
                            plotL3++;
                        }
                        else
                        {
                            MessageBox.Show("請使用下列按鈕\n繼續你的戰鬥吧");
                            對話完畢開啟按鈕();
                            BOSS2();
                            richTextBox1.Text = "遇到 LV " + 怪物等級 + " " + monsterArray[MonAryRondam] + " ，怪物血量為" + monster + "\n" + line + "\n";//顯示新怪物血量資訊               
                            label21.Text = monster + "/" + newmonster;
                            button1.Enabled = true;
                            SkillA1.Enabled = true;
                            GO.Enabled = false;
                            劇情進度 = 4;
                        }
                        break;
                    case 4://第二段劇情                        
                        richTextBox2.Text = plot4[plotL4];//第二段劇情
                        //--觸發事件-------------------------------------------------------
                        if (plotL4 == 1)
                        {
                            MonsterBar1.Maximum = 100;
                            MonsterBar1.Value = 100;//魔王血量回滿
                        }                           
                        if (plotL4 == 5)                       
                            progressBar1.Value = 0;//將主角血量歸0    
                        if (plotL4 == 7)
                            MonsterBar1.Value = 80;//魔王小扣血
                        if (plotL4 == 8)
                            MonsterBar1.Value = 70;//魔王小扣血
                        if (plotL4 == 11)
                            MonsterBar1.Value = 0;//魔王死亡
                        //-----------------------------------------------------------------
                        if (plotL4 < plot4.Length - 1)
                        {
                            plotL4++; 
                        }
                        else
                        {
                            MessageBox.Show("The end");                           
                        }
                        break;
                }
            }
            
            
           

        }//對話鍵

        public int 主角總血量;
        public int 主角目前血量;
        public int 收集的靈魂數;
        SoundPlayer A;

        private void Form1_Load(object sender, EventArgs e)
        {
            
            MessageBox.Show("請先對話後開始遊戲");
            progressBar1.Maximum = 100;//設置怪物血量最大長度值 
            主角總血量 = 100;
            主角目前血量 = 100;
            收集的靈魂數 = 0;
            技能B回復倍率 = 5;
            技能B回復血量 = level * 技能B回復倍率;
            progressBar1.Value = 主角目前血量;//設置當前值         
            progressBar2.Maximum = 3; //技能B補血 收集3三靈魂可回血           
            progressBar2.Value = 收集的靈魂數;
            A = new SoundPlayer(@"mu1.wav");
            A.PlayLooping();
        }
        public string[] plot1 = new string[]
        {
             
             "這是什麼世界.....\n難道說我死了?\n我記得我剛剛被車撞",
             "神:\n歡迎來到新世界 旅人\n因為你在凡間都會扶老奶奶過馬路\n因此你得到一次重生的機會",
             "主:\n重生的機會?",
             "神:\n沒錯\n但不是在原本的世界\n而是在夢幻明園",
             "主:\n夢幻明園...?",
             "神:\n夢幻明園是另一個平行宇宙\n你將在那重生獲得新生命\n這裡有幾把武器可以讓你選擇",
             "主:\n挖這把好帥喔",
             "神:\n那這把就屬於你了\n屋啦吧咖",
             "男主被神重生在了夢幻明園\n男主摸著頭說著",
             "主:\n奇怪了 問個為什麼要帶武器\n就被莫名重生了",
             "此時男主注意了旁邊奇怪的招牌\n\n招牌: 經滅異獸 打倒魔王 讓人類重返榮耀",
             "主:\n原來阿\n這個世界被異獸給侵入了\n所以才要給我武器重生\n",
             "主:\n被擺一道了\n算了 這應該跟遊戲一樣簡單",
             "遠方傳來呼救聲\n\n",
             "女:\n救命啊！ 有隻異獸正在追著我",
             "異獸:\n你叫破喉嚨都沒用的\n沒人會來救你",
             "主:\n什麼！一重生就遇到異獸襲擊人類\n那我...還是避開好了",
             "女:\n救命啊！ 救命啊！ \n(喊得愈來愈大聲了)",
             "主:\n不過那女生長的蠻正的\n不如...用那隻異獸來試試看我的武器好了",
             "男主跑上前擋在女主面前面對著異獸",
             "戰鬥開始！！！\n(下一段劇情將在LV10後開啟)",

        };
        public string[] plot2 = new string[]
        {
             "女主:\n謝謝你\n沒有你的話 我應該活不過今天\n我叫做小晨 很高興認識你",
             "主:\n沒事的 這些怪物都很弱哈哈(咳嗽)\n這把武器也太強大了吧(心裡想)\n我剛只是輕輕揮就秒了一堆怪物(心裡想)",
             "小晨:\n我一定要報答你\n來我家吧 我請你吃晚餐",
             "主:\n哇 天降的福利(心裡想)\n好啊",
             "小晨:\n跟我來吧\n",
             "前往小晨住處",
             "遠方傳來一陣窸窣聲",
             "刀疤土雞:\n真不知道魔王在想什麼\n明明就快攻下明園了\n結果說什麼預知到有勇者降臨\n就跑出去了",
             "主:\n等等前面好像有什麼\n是異獸!!!\n小晨快躲來我後面",
             "刀疤土雞:\n你是什麼人物\n還不讓開\n沒聽過我的名號黃金刀疤土雞?",
             "主:\n什麼黃金狗雞",
             "刀疤土雞:\n看來你是不想活了",
             "戰鬥開始！！！",

        };
        public string[] plot3 = new string[]
        {
             "主:\n這黃金狗雞 真難纏(咳嗽)\n接了我這麼多招才死\n不過你沒事吧 小晨",
             "主:\n我剛好像看你搖飲料\n搖到手斷掉了...",
             "小晨:\n沒事 我完好無缺阿\n應該是你看錯了",
             "主:\n沒事就好 繼續走吧",
             "小晨:\n好",
             "主:\n(咳嗽)...\n\n到了女主家...",
             "主:\n咦~這裡也太不像住家了吧小晨",
             "男主此時轉頭\n看到了變異的小晨\n\n小晨:\n這才是我真實的模樣",
             "主:\n不會吧原來...\n你才是魔王\n剛剛你的手的確斷了\n難怪...",
             "小晨:\n現在才發現太晚了\n前幾天我預知到的勇者就是你啊",
             "主:\n你不會得逞的",
             "小晨:\n來試試看阿",
             "戰鬥開始！！！",
        };
        public string[] plot4 = new string[]
        {
             "主:\n什麼大魔王\n原來也才這樣",//0
             "此時大魔王的屍體\n開始慢慢融合了",//1
             "主:\n什麼!\n為什麼你還可以復活\n我明明幹掉你了",//2
             "大魔王:\n你太小看我了\n我可是大魔王",//3
             "大魔王:\n你看過哪個故事的大魔王這麼容易死的\n\n大魔王說完直接一刀秒殺了男主",//4
             "主:\n不可能啊...(咳嗽)",//5主死去
             "大魔王:\n哼哼",//6
             "此時大魔王感到身體不適",//7扣血
             "大魔王:\n我怎麼了(咳嗽)\n身體開始發燒 開始咳嗽了",//8扣血
             "主:\n我想到了 我身前不是被車撞死的\n而是被武漢病毒幹掉的\n沒想到病毒跟我進來這個世界了",//9
             "主:\n看來你也被傳染了\n你日子不多了 大魔王",//10
             "大魔王:不可能的\n區區這種小病毒怎麼可能\n\n大魔王說完就倒下了 主角也因失血過多而死...",//11魔王死
             "沒想到魔王體內的病毒開始蔓延\n現在整個夢幻明園都被武漢病毒感染了...",//12
        };

        private void GO_Click(object sender, EventArgs e)
        {
            if (monster <= 0)
            {
                Strange();//生成怪物
            }
            Random Rnd = new Random();//寫入內建亂數產生class
            MonAryRondam = Rnd.Next(0, monsterArray.Length-3);//怪物陣列亂數產生 最後兩格放魔王跟幹部
            MonsterBar1.Maximum = newmonster;
            MonsterBar1.Value = newmonster;
            richTextBox1.Text = "遇到 LV " + 怪物等級+" "+monsterArray[MonAryRondam] + " ，怪物血量為" + monster + "\n" + line + "\n";//顯示新怪物血量資訊               
            label21.Text = monster + "/" + newmonster;
            button1.Enabled = true;
            SkillA1.Enabled = true;
            GO.Enabled = false;
        }//前進遇怪
        public int 技能B回復血量;
        public int 技能B回復倍率;
        private void button4_Click(object sender, EventArgs e)//治療術
        {
            if (收集的靈魂數 == 3)
            {
                主角目前血量 = 主角目前血量 + (技能B回復血量);//回復等級*15的血量                
                if (主角目前血量 > 主角總血量)
                {
                    主角目前血量 = 主角總血量;
                }
                progressBar1.Value = 主角目前血量;
                label20.Text = 主角目前血量 + "/" + 主角總血量;
                收集的靈魂數 = 0;
                richTextBox1.Text = "使用靈魂萃取~回復" + 技能B回復血量 + "的血量~目前血量" + 主角目前血量 + "\n";
            }
            progressBar2.Value = 收集的靈魂數;
        }

        private void button11_Click(object sender, EventArgs e)//生命藥水
        {
            if (gold >= 20)
            {
                gold -= 20;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                主角目前血量 += 50;
                if (主角目前血量 > 主角總血量)
                {
                    主角目前血量 = 主角總血量;
                }
                progressBar1.Value = 主角目前血量;
                label20.Text = 主角目前血量 + "/" + 主角總血量;
                richTextBox1.Text = "\n回復了50D血\n";
            }
            else
            {
                MessageBox.Show("可憐~錢不夠");
            }

        }
        public int 升級技能A加成=0;
        private void button5_Click(object sender, EventArgs e)//升級技能A
        {
            if (gold >= 35)
            {
                gold -= 35;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                richTextBox1.Text = "以購買升級技能A(增加20攻擊)，花費20元";                
                升級技能A加成 += 20;
            }
            else
            {
                MessageBox.Show("可憐~錢不夠");
            }
        }

        private void button6_Click(object sender, EventArgs e)//升級治療術
        {
            if (gold >= 15)
            {
                gold -= 15;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                技能B回復倍率 += 1;//升級倍率
                技能B回復血量 += level * 技能B回復倍率;
                button4.Text = "治癒術~回復" + 技能B回復血量 + "的血量";
                richTextBox1.Text = "\n升級了技能B~現在可回復" + 技能B回復血量 + "的血量\n";
            }
            else
            {
                MessageBox.Show("可憐~錢不夠");
            }

        }

        private void button9_Click(object sender, EventArgs e)//攻擊藥水
        {
            if (gold >= 20)
            {
                gold -= 20;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                雙倍攻擊持續時間 = 5;
                richTextBox1.Text = "以購買攻擊雙倍加成5回合，花費20元";
                label22.Text = "剩餘回合數:" + 雙倍攻擊持續時間;
            }
            else
            {
                MessageBox.Show("可憐~錢不夠");
            }
        }
        private bool check(ref int 持續時間)//x代表剩餘回合
        {
            bool check = false;
            if (持續時間 > 0) //>0代表有回合數
            {
                持續時間--;
                label22.Text = "剩餘回合數:" + 雙倍攻擊持續時間;
                check = true;
                return check;
            }
            else
            {
                check = false;
                return check;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

                MessageBox.Show("請使用下列按鈕\n開始你的戰鬥吧");
                對話完畢開啟按鈕();
                劇情進度 = 2;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            level = 10;
            ExpLevel();//升等
            updatedata();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random();//寫入內建亂數產生class
            角色名稱亂數 = Rnd.Next(0, 角色名稱生成.Length-1);//陣列亂數產生
            textBox1.Text=角色名稱生成[角色名稱亂數];

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random();//寫入內建亂數產生class
            角色名稱亂數 = Rnd.Next(0, 角色名稱生成.Length -1);//陣列亂數產生
            textBox1.Text = 角色名稱生成[角色名稱亂數];
        }

        private void button14_Click(object sender, EventArgs e)//寵物A
        {
            if (gold >= 100)
            {
                gold -= 100;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                寵物種類 = 1;
                寵物攻擊力 = (int)(attack3 / 2);
                寵物攻速 = 1;
                MessageBox.Show("牠會成為你的得力助手!");
                button15.Enabled = false;
                button14.Enabled = false;
                button17.Enabled = true;
                買哪隻寵物 = 1;
            }
            else
            {
                MessageBox.Show("可憐~錢不夠");
            }
            
        }

        private void button15_Click(object sender, EventArgs e)//寵物B
        {
            if (gold >= 150)
            {
                gold -= 150;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                寵物種類 = 2;
                寵物攻擊力 = (int)(attack3 * 5);
                寵物攻速 = 4;
                MessageBox.Show("牠會成為你的得力助手!");
                button14.Enabled = false;
                button15.Enabled = false;
                button18.Enabled = true;
                買哪隻寵物 = 1;
            }
            else
            {
                MessageBox.Show("可憐~錢不夠");
            }

            
        }

        private void button16_Click(object sender, EventArgs e)//test
        {
            gold = gold + 100;
            goldText.Text = "" + gold;//更新金幣狀態列

            goldText2.Text = "" + gold;//更新第二金幣狀態列
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要賣掉疾風豹嗎?", "賣掉寵物", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                gold = gold + 80;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                button14.Enabled = true;
                button15.Enabled = true;

                button17.Enabled = false;
                寵物種類 = 0;

                寵物攻擊力 = 0;
                寵物攻速 = 0;
                買哪隻寵物 = 0;
            }
            else
            {
                
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要賣掉鐵角犀牛嗎?", "賣掉寵物", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                gold = gold + 130;
                goldText.Text = "" + gold;//更新金幣狀態列
                goldText2.Text = "" + gold;//更新第二金幣狀態列
                button14.Enabled = true;
                button15.Enabled = true;

                button18.Enabled = false;
                寵物種類 = 0;
                寵物攻擊力 = 0;
                寵物攻速 = 0;
                買哪隻寵物 = 0;
            }
            else
            {

            }
        }
    }
}
