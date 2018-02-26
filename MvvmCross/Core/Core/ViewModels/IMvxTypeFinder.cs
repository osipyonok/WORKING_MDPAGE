// IMvxTypeFinder.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;

namespace MvvmCross.Core.ViewModels
{
    public interface IMvxTypeFinder
    {
        Type FindTypeOrNull(Type candidateType);
    }
}