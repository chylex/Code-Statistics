using CodeStatistics.ConsoleUtil;

namespace CodeStatistics.Handlers{
    interface IHandlerTab{
        string GetName();
        void RenderInfo(ConsoleWrapper c, int y);
    }
}
