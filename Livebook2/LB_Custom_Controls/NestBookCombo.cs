using System;
using LiveDLL;

namespace LiveBook
{
    public class NestBookCombo : System.Windows.Forms.ComboBox
    {
        
        
        private bool _IncludeAllStrats = true;

        public bool IncludeAllStrats
        {
            get { return _IncludeAllStrats; }
            set { _IncludeAllStrats = value; ChangePortfolio();}
        }

        private int _Id_Portfolio = 43;

        public int Id_Portfolio
        {
            get { return _Id_Portfolio; }
            set { _Id_Portfolio = value; ChangePortfolio(); }
        }

        public NestBookCombo()
        {
            ChangePortfolio();
        }

        public void ChangePortfolio()
        {
            int curValue = -1;
            if (this.SelectedValue != null)
            {
                curValue = Convert.ToInt32(this.SelectedValue.ToString());
            }
            if (_IncludeAllStrats)
            {
                LiveDLL.FormUtils.LoadCombo(this, "SELECT -1 AS Id_Book, 'All Books' AS Book UNION SELECT [Id Book] AS Id_Book, Book from NESTRT.dbo.Tb000_Posicao_Atual (nolock) WHERE [Id Portfolio]=" + _Id_Portfolio + " GROUP BY [Id Book], Book", "Id_Book", "Book", curValue);
            }
            else
            {
                LiveDLL.FormUtils.LoadCombo(this, "SELECT [Id Book] AS Id_Book, Book from NESTDB.dbo.Tb000_Historical_Positions (nolock) WHERE [Id Portfolio]=" + _Id_Portfolio + " GROUP BY [Id Book], Book", "Id_Book", "Book", curValue);
            }
        }
    }
}
