using System.Linq;
using Sitecore.ContentTesting.Helpers;
using Sitecore.ContentTesting.Inspectors;
using Sitecore.ContentTesting.Pipelines.StartPageTest;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Publishing;

namespace Sitecore.Support.ContentTesting.Pipelines.StartPageTest
{
  public class DeployPageTestDependencies : StartPageTestProcessor
  {
    public override void Process(StartPageTestArgs args)
    {
      Database[] targets = PublishingHelper.GetTargets(args.HostItem).ToArray<Database>();
      foreach (DataUri uri in new TestVariablesInspector().GetContentTestDataSources(args.Test.PageLevelTestVariables))
      {
        Item item = args.HostItem.Database.GetItem(uri);
        if (item != null)
        {
          Language[] languages = new Language[] { args.HostItem.Language };
          PublishManager.PublishItem(item, targets, languages, false, true);
        }
      }
    }
  }
}
