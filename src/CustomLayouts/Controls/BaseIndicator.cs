using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts
{
    /// <summary>
    /// all indicator need to inherit the interface of View
    /// <scc cref="View"/>
    /// </summary>
    public interface BaseIndicator : IViewController, IVisualElementController, IElementController
    {

    }
}
