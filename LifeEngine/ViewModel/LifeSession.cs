using LifeEngine.Model;

namespace LifeEngine.ViewModel
{
    public class LifeSession
    {
        public Field CurrentField { get; set; }

        public LifeSession(bool[,] boolField)
        {
            int i, j;
            Cell[,] cells = new Cell[boolField.GetLength(0), boolField.GetLength(1)];

            for (i = 0; i < boolField.GetLength(0); i++)
            {
                for (j = 0; j < boolField.GetLength(1); j++)
                {
                    cells[i, j] = new Cell { Mode = (boolField[i, j] == true) ? CellMode.Alive : CellMode.Dead };
                }
            }

            CurrentField = new Field(cells);
        }

        public void Update() => CurrentField.Update();
    }
}
