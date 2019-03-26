using System.Collections.Generic;


public class UpdateRegistry : Singleton<UpdateRegistry>
{
    public abstract class IStaticUpdate
    {
        public void Init() => Instance.Register(this);
        public abstract void Update();
    }
    
    private List<IStaticUpdate> registry;

    public void Register(IStaticUpdate updateable)
    {
        if (registry == null)
            registry = new List<IStaticUpdate>();

        registry.AddOnce(updateable);
    }

    private void Update()
    {
        for (int i = registry.Count - 1; i >= 0; i -= 1)
        {
            if (registry[i] == null)
            {
                registry.RemoveAt(i);
                i -= 1;
                continue;
            }

            registry[i].Update();
        }
    }
}