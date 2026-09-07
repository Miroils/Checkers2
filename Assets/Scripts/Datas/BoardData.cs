using System.Collections.Generic;

public class BoardData
{
    public Cell[,] Cells { get; set; }
    public List<Checker> RedCheckers { get; set; }
    public List<Checker> GreenCheckers { get; set; }
}