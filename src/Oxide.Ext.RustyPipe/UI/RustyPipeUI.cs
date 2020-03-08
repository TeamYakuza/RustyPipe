namespace Oxide.Ext.RustyPipe.UI
{
    public class RustyPipeUI
    {
        public RustyPipeHud CreateHud(BasePlayer player, int width = 1920, int height = 1080,IRustyPipeTheme theme=null)
        {
            return new RustyPipeHud(player,width,height,theme);
        }
    }
}
