using Microsoft.Maui.Layouts;

namespace CustomLayouts;

public class HorizontalWrapLayout : StackLayout
{
    public HorizontalWrapLayout()
    {
    }

    protected override ILayoutManager CreateLayoutManager() => new HorizontalWrapLayoutManager(this);
}
