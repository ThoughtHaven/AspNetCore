namespace ThoughtHaven.AspNetCore.Redirects
{
    public class RedirectRoute
    {
        public string Template { get; }
        public string Location { get; }
        public bool Permanent { get; }

        public RedirectRoute(string template, string location, bool permanent = false)
        {
            this.Template = Guard.NullOrWhiteSpace(nameof(template), template);
            this.Location = Guard.NullOrWhiteSpace(nameof(location), location);
            this.Permanent = permanent;
        }
    }
}