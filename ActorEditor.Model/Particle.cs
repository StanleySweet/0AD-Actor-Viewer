namespace ActorEditor.Model
{
    public class Particle
    {
        private string _relativePath;

        public Particle(string relativePath)
        {
            this._relativePath = relativePath;
        }

        public string RelativePath {
            get { return this._relativePath; }
            set { this._relativePath = value; }
        }
    }
}
