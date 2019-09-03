# WorkerDock development guidelines
Before reading this guidelines book, we highly recommend you to read the [About WorkerDock]() first.
## Who need to read these guidelines ?
We need you to read this book if you:
1. Are a WorkerDock developer; or
2. Are contributing to our repository (directly or indirectly); or
3. Want to understand to philosophy behind the app.
## What does WorkerDock do ?
WorkerDock is an advanced, modularly-integrated and easy-to-use console application.
As the name suggests, it is the perfect companion for you, the Workers!
As a side note, it is specifically designed for software engineer, hard-core computer user...
### Functionality
* Create, organize and share your notes (over local network).
* Great alarming system, in case you forget to attend the meeting.
* Store encrypted data, things you do not want others to see!
and more, to be implemented in the future ...
## Internal tasks
In case of you not understanding the engine by looking at the source, we will help you!
WorkerDock is basically a console application (self-implemented), which covers a modularly-integrated system of plugins.
Plugins are just smaller bunch of apps, each serves a particular reason.

For example, the BasePlugin takes control of the basic control for the console.
Its jobs (commands) include ```clear```, ```exit``` and so on...

Each command entered will go through 3 main stages:
1. The first one is parsing, it helps seperate the command into individual sub-command and corresponding parameters.
2. Second is plugin selection, each command starts with the plugin's "name" (InvokeID), so we can easily determine what plugin to use.
3. And lastly, the plugin execution itself! The plugin engine will be given the sub-command and parameters and execute them!

After that, the plugin itself can either return the value to the main controller or print the result directly to console.
## Technical design
This will be a bit more advanced!
### The console
The control itself is a big ```while``` loop.

On console startup, it calls ```PreStartupSetup```. Then, its enter the main loop.
#### Pre-startup setup
##### Tasks
* Load plugins
* Load prompts
##### Development note
*READ-ONLY (minor only)*: Any change to this is an "upgrade".
Therefore, unless a new plugin is approved and set as default, no extra change is needed.

#### Main control loop
##### Tasks
* Reload the prompt
* Print the prompt
* Get user input
* Parsing input
* Pass command to corresponding plugin
and repeat.
##### Development note
*READ-ONLY (major only)*: The current loop is considered "finished".
Therefore, unless major changes in the PluginObject base class, no extra change is needed.

### ```PluginObject``` class
The ```PluginObject``` class is an abstract base class for all plugins.
All plugin *MUST* implement PluginObject (and optionally, other interfaces).
##### Tasks
* Provide basic metadata
    * Name
	* Version
	* InvokeID
	* CallableCommand
* Provide ```run``` method, which is used to execute any given command.
##### Development note
*READ-ONLY (breaking only)*: Because it is used by other plugins, any change made to this class is considered a "breaking change".
Therefore, it is highly *NOT RECOMMENDED* to change this class.

### Plugins
Plugins are the fundamental building blocks of the application. Each plugin serves a particular task.
Combined together, they create a perfect ecosystem for our usage.
#### Available plugins
* [BasicPlugin]()
#### Plugin design
Example:
```csharp
namespace WorkerDock.Plugins
{
	public sealed class MyPlugin : PluginObject
	{
		// Implementation
	}
}
```
First, all plugins must live in the ```WorkerDock.Plugins``` namespace. Then, a sealed class is optional, but highly recommended.
As we mentioned before, all plugins must implement the ```PluginObject``` base class.

Both in-development and approved plugins must live in their own directory (or "home", named the same as plugin name).
Which in turn lives inside ```Plugins``` directory.

Plugins can store anything inside their own home (only outside if dependent of other plugins), including configuration, plugin data, etc.
#### Development note
*RECOMMENDED*: We highly recommend you to make new plugins to add to the ecosystem.