public abstract class TaskGraph : AIGraph
{
    private Task root;
    public Task Root => root;

    protected void OnEnable()
    {
        Task sequenceMoveToRoom = new Sequence();
        sequenceMoveToRoom.AddChildren(new DoorOpenCondition("Door1"));
        sequenceMoveToRoom.AddChildren(new FollowTask("Room1"));

        Task sequenceOpenDoorMoveToRoom = new Sequence();
        sequenceOpenDoorMoveToRoom.AddChildren(new FollowTask("Door1"));
        sequenceOpenDoorMoveToRoom.AddChildren(new FollowTask("Room1"));
        
        root = new Selector();
        Root.AddChildren(sequenceMoveToRoom);
        Root.AddChildren(sequenceOpenDoorMoveToRoom);
    }
}