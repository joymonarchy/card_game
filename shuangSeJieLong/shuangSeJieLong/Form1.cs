using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shuangSeJieLong
{
    
    public partial class Form1 : Form
    {
        class card
        {
            public int index;//纸牌编号，一共52张
            public bool showFace;//纸牌正反
            public int pile;//纸牌的堆数，12堆为黑，三四堆为红
            public int cardNum;//纸牌的点数
            public bool isColorRed;//纸牌的颜色
            public bool canPlace;
            public Point leftUpPoint;
            public Image img;

        }
        class place
        {
            public Point point;
            public card placedCard;
            public bool isPlaced;//有没有被放牌
        }

        Point point_mousedown_on_panel;
        place[,] place_7row_initial_pozition = new place[7, 52];
        Point[,] point_7row_initial_pozition = new Point[7,52];
        Point[] point_5ready_initial_pozition = new Point[5];
        card[] cards = new card[52];
        

        void shuffle()//洗牌,
        {
            Random rnd = new Random();
            int[] shuffleNum = new int[26];
            for(int i=0;i<26; i++)
            {
                shuffleNum[i] = 2;
            }
            for(int i = 0; i < 52;i++)
            {
                cards[i] = new card();
                cards[i].index = i;
                int tmp = rnd.Next(0, 26);

                while (shuffleNum[tmp] <= 0)
                {
                    tmp = rnd.Next(0, 26);
                }

                cards[i].cardNum = tmp/2+1;

                int color = tmp % 2;
                if(color == 1) { cards[i].isColorRed = false; }
                else
                {
                    cards[i].isColorRed = true;
                }
                
                shuffleNum[tmp]--;
            }
        }

        void dealInitial()//最开始发牌
        {
            for(int i = 0; i < 52; i++)
            {
                if (i >= 7&&i<17)
                {
                    cards[i].showFace = true;
                }
                else
                {
                    cards[i].showFace = false ;
                }
                if (i >= 10 && i < 17)
                {
                    cards[i].canPlace = true;
                }
                else
                {
                    cards[i].canPlace = false; 
                }
            }
        }

        void deal(int n)//共有五次加牌机会
        {
            for(int i = 0; i < 52; i++)
            {
                cards[i].canPlace = false;//所有先不可放
            }
            for(int i = 17+(n-1)*7,count = 0; count < 7; count++,i++)
            {
                cards[i].showFace = true;
                cards[i].canPlace = true;//发牌后的肯定在最下层所以可放
                int line = 0;
                while (place_7row_initial_pozition[count, line].isPlaced)
                {
                    line++;
                }
                cards[i].leftUpPoint = place_7row_initial_pozition[count, line].point;
                place_7row_initial_pozition[count, line].isPlaced = true;
                place_7row_initial_pozition[count, line].placedCard = cards[i];
                sort();
                draw();
            }
        }

        void set_point_7row_initial_position()//上面牌的位置
        {
            for(int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 52; j++) 
                {
                    Point point = new Point(i * 75, j * 30 + 10);
                    point_7row_initial_pozition[i,j] = point;
                }
            }
        }

        void set_place_7row_initial_position()
        {
            for(int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 52; j++)
                {
                    place_7row_initial_pozition[i, j] = new place();
                    place_7row_initial_pozition[i, j].placedCard = new card();
                    place_7row_initial_pozition[i, j].point = point_7row_initial_pozition[i, j];
                    place_7row_initial_pozition[i, j].isPlaced = false;
                }
            }
        }

        void set_point_5ready_initial_position()//发牌的位置
        {
            for(int i = 0; i < 5; i++)
            {
                point_5ready_initial_pozition[i] = new Point(600 + 30 * i, 500);
            }
        }

        void pick_card_img(card pickedCard)
        {
            if (pickedCard.isColorRed)
            {
                switch (pickedCard.cardNum)
                {
                    case 1: pickedCard.img = Properties.Resources.heart1;  break;
                    case 2: pickedCard.img = Properties.Resources.heart2;  break;
                    case 3: pickedCard.img = Properties.Resources.heart3;  break;
                    case 4: pickedCard.img = Properties.Resources.heart4;  break;
                    case 5: pickedCard.img = Properties.Resources.heart5;  break;
                    case 6: pickedCard.img = Properties.Resources.heart6;  break;
                    case 7: pickedCard.img = Properties.Resources.heart7;  break;
                    case 8: pickedCard.img = Properties.Resources.heart8;  break;
                    case 9: pickedCard.img = Properties.Resources.heart9;  break;
                    case 10: pickedCard.img = Properties.Resources.heart10;  break;
                    case 11: pickedCard.img = Properties.Resources.heart11;  break;
                    case 12: pickedCard.img = Properties.Resources.heart12;  break;
                    case 13: pickedCard.img = Properties.Resources.heart13;  break;
                }

            }
            else
            {
                switch (pickedCard.cardNum)
                {
                    case 1: pickedCard.img = Properties.Resources.spade1;  break;
                    case 2: pickedCard.img = Properties.Resources.spade2;  break;
                    case 3: pickedCard.img = Properties.Resources.spade3;  break;
                    case 4: pickedCard.img = Properties.Resources.spade4;  break;
                    case 5: pickedCard.img = Properties.Resources.spade5;  break;
                    case 6: pickedCard.img = Properties.Resources.spade6;  break;
                    case 7: pickedCard.img = Properties.Resources.spade7;  break;
                    case 8: pickedCard.img = Properties.Resources.spade8;  break;
                    case 9: pickedCard.img = Properties.Resources.spade9;  break;
                    case 10: pickedCard.img = Properties.Resources.spade10;  break;
                    case 11: pickedCard.img = Properties.Resources.spade11;  break;
                    case 12: pickedCard.img = Properties.Resources.spade12;  break;
                    case 13: pickedCard.img = Properties.Resources.spade13;  break;
                }
            }
        }

        void initial_sort()
        {
            for(int i = 0; i < 52; i++)//刚开始排顺序
            {

                if (cards[i].index < 17)
                {
                    int row = (cards[i].index ) % 7;
                    int line = (cards[i].index ) / 7;
                    cards[i].leftUpPoint = point_7row_initial_pozition[row,line];
                    place_7row_initial_pozition[row, line].placedCard = cards[i];
                    place_7row_initial_pozition[row, line].isPlaced = true;
                    if (cards[i].index < 17 && cards[i].index > 9)
                    {
                        cards[i].canPlace = true;
                    }
                    else
                    {
                        cards[i].canPlace = false;
                    }
                }
                else
                {
                    int num = cards[i].index - 17;
                    int sum = num / 7;
                    cards[i].leftUpPoint = point_5ready_initial_pozition[sum];
                }
                if(cards[i].showFace == false)//排图案的
                {
                    cards[i].img = Properties.Resources.back;
                }
                else
                {
                    pick_card_img(cards[i]);
                }
            }
        }

        void draw()
        {
            Graphics g = panel.CreateGraphics();
            Brush brush = new SolidBrush(Color.Green);
            g.FillRectangle(brush, new Rectangle(0, 0, panel.Width, panel.Height));
            for (int i = 17; i < 52; i += 7)
            {
                if (!cards[i].showFace)
                {
                    g.DrawImage(cards[i].img, cards[i].leftUpPoint);
                }
            }

            for (int i = 0; i < 7; i++)
            {
                int j = 0;
                while (place_7row_initial_pozition[i, j].isPlaced)
                {
                    Image img = place_7row_initial_pozition[i, j].placedCard.img;
                    g.DrawImage(img, place_7row_initial_pozition[i, j].point);
                    j++;
                }
            }
            g.Dispose();
            GC.Collect();

        }

        void sort()
        {

            for (int i = 0; i < 52; i++)
            {
                if (cards[i].showFace)
                {
                    pick_card_img(cards[i]);
                }
            }
        }

        bool check_win()
        {
            for(int row = 0; row < 7; row++)
            {
                int line = 0;
                int number = 13;
                while (place_7row_initial_pozition[row, line].isPlaced)//当格子上面有牌的时候
                {
                    if(place_7row_initial_pozition[row,line].placedCard.cardNum != number)
                    {
                        return false;
                    }
                    else
                    {
                        line++;
                        number--;
                    }
                }
            }
            return true;
        }

        public Form1()
        {
            InitializeComponent();
            set_point_7row_initial_position();
            set_point_5ready_initial_position();
            set_place_7row_initial_position();
            shuffle();
            dealInitial();
            initial_sort();
            draw();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            shuffle();
            dealInitial();
            initial_sort();
            draw();
            
            button1.Visible = false;
            
        }

        void move(card card_from,place place_to,bool isK)//下面一行都move，上面的如果盖着就反过来
        {
            Point selected_card = card_from.leftUpPoint;
            int row_from = selected_card.X/75;
            int line_from = selected_card.Y/30;
            int row_to = place_to.point.X / 75;
            int line_to = place_to.point.Y / 30;
            int i = line_from;
            int j = line_to;


            if (line_from > 0) //如果被移动的上方是牌,
            {
                if (place_7row_initial_pozition[row_from, line_from - 1].isPlaced)
                {
                    card tmp_card = new card();
                    tmp_card = place_7row_initial_pozition[row_from, line_from - 1].placedCard;
                    tmp_card.canPlace = true;//自动翻转，下层可放
                    tmp_card.showFace = true;
                    
                    
                } 
            }

            if (!isK)//不是k时
            {
                place_to.placedCard.canPlace = false;
                while (place_7row_initial_pozition[row_from, i].isPlaced)
                {
                    card_from = place_7row_initial_pozition[row_from, i].placedCard;
                    place_7row_initial_pozition[row_from, i].isPlaced = false;
                    card_from.leftUpPoint = place_7row_initial_pozition[row_to, j + 1].point;

                    place_7row_initial_pozition[row_to, j + 1].isPlaced = true;
                    place_7row_initial_pozition[row_to, j + 1].placedCard = card_from;
                    i++;
                    j++;
                }
                
            }
            else
            {
                while (place_7row_initial_pozition[row_from, i].isPlaced)
                {
                    card_from = place_7row_initial_pozition[row_from, i].placedCard;
                    place_7row_initial_pozition[row_from, i].isPlaced = false;
                    card_from.leftUpPoint = place_7row_initial_pozition[row_to, j].point;

                    place_7row_initial_pozition[row_to, j ].isPlaced = true;
                    place_7row_initial_pozition[row_to, j ].placedCard = card_from;
                    i++;
                    j++;

                }
            }
            sort();
            draw();
            //draw(cards[51]);

        }

        card select_card(int e_x,int e_y)//检测被选中的牌
        {
            card select_card = new card();
            select_card.showFace = false;
            for(int i = 0; i < 52; i++)
            {
                if (e_x >= cards[i].leftUpPoint.X && e_x <= cards[i].leftUpPoint.X + 71)
                {
                    if (cards[i].canPlace)
                    {
                        if(e_y>= cards[i].leftUpPoint.Y && e_y <= cards[i].leftUpPoint.Y + 96)
                        {
                            select_card = cards[i];
                        }
                    }
                    else
                    {
                        if(e_y >= cards[i].leftUpPoint.Y && e_y <= cards[i].leftUpPoint.Y + 30)
                        {
                            select_card = cards[i];
                        }
                    }
                }
            }

            return select_card;
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            point_mousedown_on_panel = new Point(e.X, e.Y);
            card selected_card = select_card(point_mousedown_on_panel.X, point_mousedown_on_panel.Y);
            if (selected_card.index >= 17 && selected_card.showFace==false)//发牌
            {
                if (selected_card.index < 24 && selected_card.showFace == false)
                {
                    deal(1);//第一次发牌
                }
                else if (selected_card.index < 31 && selected_card.showFace == false && cards[selected_card.index - 7].showFace)
                {
                    deal(2);
                }
                else if (selected_card.index < 38 && selected_card.showFace == false && cards[selected_card.index - 7].showFace)
                {
                    deal(3);
                }
                else if (selected_card.index < 45 && selected_card.showFace == false && cards[selected_card.index - 7].showFace)
                {
                    deal(4);
                }
                else if (selected_card.index < 52 && selected_card.showFace == false && cards[selected_card.index - 7].showFace)
                {
                    deal(5);
                }
            }
            else {
                if (selected_card.showFace)//如果可以点击
                {
                    if (selected_card.cardNum != 13)
                    {
                        for (int i = 0; i < 52; i++)
                        {
                            if (cards[i].cardNum == selected_card.cardNum + 1 && cards[i].canPlace && cards[i].isColorRed == selected_card.isColorRed && cards[i].leftUpPoint.X != selected_card.leftUpPoint.X)
                            {//可以动
                                int row = cards[i].leftUpPoint.X / 75;
                                int line = cards[i].leftUpPoint.Y / 30;
                                move(selected_card, place_7row_initial_pozition[row, line],false);
                                if (check_win())
                                {
                                    MessageBox.Show("恭喜，你赢了！");
                                }
                                break;

                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if (!place_7row_initial_pozition[i, 0].isPlaced)
                            {
                                move(selected_card, place_7row_initial_pozition[i, 0],true);

                            }
                        }
                    }
                }


            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            shuffle();
            set_point_7row_initial_position();
            set_point_5ready_initial_position();
            set_place_7row_initial_position();
            dealInitial();
            initial_sort();
            draw();
        }

        private void btn_rule_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Resources.rule);
        }
    }
}
