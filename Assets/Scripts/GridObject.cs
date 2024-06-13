public class GridObject
{
    private GridPosition gridPosition;
    private GridSystem gridSystem;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }
}
