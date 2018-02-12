namespace ActorEditor.Model
{
    public class Particle
    {
        private string _relativePath;

        public Particle(string relativePath)
        {
            this._relativePath = relativePath;
        }

        public string GetRelativePath()
        { return this._relativePath; }

        public void SetRelativePath(string value)
        { this._relativePath = value; }
    }
}
