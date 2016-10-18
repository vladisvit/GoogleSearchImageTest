using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GoogleSearchImageTest
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/bower_components/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
            "~/bower_components/angular/angular.js",
            "~/bower_components/angular-sanitize/angular-sanitize.js",
            "~/bower_components/angular-animate/angular-animate.js",
            "~/bower_components/angular-cookies/angular-cookies.js",
            "~/bower_components/angular-bootstrap/ui-bootstrap.js",
            "~/bower_components/angular-loading-bar/build/loading-bar.js"));

            bundles.Add(new StyleBundle("~/bundles/angular_css").Include(
                "~/bower_components/angular-loading-bar/build/loading-bar.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/bower_components/bootstrap/dist/js/bootstrap.js",
                      "~/bower_components/bootstrap.growl/dist/bootstrap-notify.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/src","*.js", true));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.css",
                      "~/bower_components/font-awesome/css/font-awesome.css",
                      "~/Content/site.css"));
        }
    }
}