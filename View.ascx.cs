﻿/*
' Copyright (c) 2018  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;
using Christoc.Modules.FacebookLeads.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Facebook;

namespace Christoc.Modules.FacebookLeads
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 
    /// Setup of Facebook App
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : FacebookLeadsModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //https://developers.facebook.com/docs/marketing-api/guides/lead-ads/quickstart/webhooks-integration
                if (!Page.IsPostBack)
                {
                    if (Settings.Contains("AppId"))
                    {
                        AppInfo.Visible = true; NoAppInfo.Visible = false;
                        if (Settings.Contains("accessToken"))
                        {

                            var accessToken = Settings["accessToken"].ToString();
                            hfAuthToken.Value = accessToken;

                        }
                        //TODO: check if the access token is setup in the Module Settings, if not, let's go through Facebook connection
                        else
                        {
                        }
                    }
                    //AppId not configured yet, we need to display a message
                    else
                    {
                        AppInfo.Visible = false; NoAppInfo.Visible = true;
                    }
                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        protected void lbAuth_Click(object sender, EventArgs e)
        {
            var accessToken = hfAuthToken.Value;
            var mc = new ModuleController();
            mc.UpdateModuleSetting(ModuleId, "accessToken", accessToken);
        }


    }
}