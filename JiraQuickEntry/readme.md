# JIRA Quick Entry Tool

I recently had to start working with JIRA for tracking project issues. I was in a situation where I needed to enter a lot of issues quickly by 
hand (I didn't have an existing .csv file or anything), and as far as I could tell, JIRA doesn't have a quick-entry feature like [FogBugz](http://www.fogcreek.com/fogbugz/) does. 
So I threw together a quick application to let me enter a bunch of issues quickly. 

The JIRA Quick Entry tool lets you rapidly enter cases into [JIRA](http://www.atlassian.com/software/jira/overview) 
via [JIRA's SOAP API](http://docs.atlassian.com/rpc-jira-plugin/latest/com/atlassian/jira/rpc/soap/JiraSoapService.html).

To set up the tool, replace the 'address' attribute in the 'endpoint' section of the app.config with the address of your JIRA instance. 

When entering an issue, the Project and Summary fields are required. You can click the 'Create' button to submit your issue or hit CTRL-ENTER.

As you add issues, their summaries will show up on the right to help you keep track of what you entered.

Known issues (stuff I haven't gotten around to fixing yet):

* There's currently no way to select an issue type - they default to 'New Feature'. I'll add a dropdown sometime in the near future.
* If you close the login dialog (instead of logging in), submitting issues will fail (and you'll have to restart to log in)
* No installer yet, so you'll have to build from source. 

I put this together pretty quickly - there are probably several other issues. If you find a bug or have a feature request, 
please [submit it](https://github.com/hartez/JIRA-Quick-Entry/issues).