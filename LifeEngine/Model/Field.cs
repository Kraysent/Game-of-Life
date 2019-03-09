namespace LifeEngine.Model
{
    public class Field
    {
        private Cell[,] _field;
        public int Width => _field.GetLength(0);
        public int Height => _field.GetLength(1);

        public Field(Cell[,] field)
        {
            _field = field;
        }

        public Cell this[int x, int y]
        {
            get => _field[x, y];
            set => _field[x, y] = value;
        }
        
        public void Update()
        {
            Cell[,] newField = new Cell[Width, Height];
            CellMode currCell;
            
            for (int l = 0; l < Width; l++)
            {
                for (int m = 0; m < Height; m++)
                {
                    if (l == 0 || l == Width - 1 || m == 0 || m == Height - 1)
                    {
                        newField[l, m] = this[l, m];
                        continue;
                    }

                    currCell = this[l, m].Mode;

                    int aliveNeighbours = 0;
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            aliveNeighbours += (int)this[l + i, m + j].Mode;

                    aliveNeighbours -= (int)this[l, m].Mode;

                    // Cell is lonely and dies 
                    if ((currCell == CellMode.Alive) && (aliveNeighbours < 2))
                        newField[l, m] = new Cell { Mode = CellMode.Dead };

                    // Cell dies due to over population 
                    else if ((currCell == CellMode.Alive) && (aliveNeighbours > 3))
                        newField[l, m] = new Cell { Mode = CellMode.Dead };

                    // A new cell is born 
                    else if ((currCell == CellMode.Dead) && (aliveNeighbours == 3))
                        newField[l, m] = new Cell { Mode = CellMode.Alive };

                    // Remains the same 
                    else
                        newField[l, m] = this[l, m];
                }
            }

            _field = newField;
        }
    }
}
