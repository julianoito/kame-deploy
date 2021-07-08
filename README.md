# kame-deploy
Kame Deploy is a end of course project created to resolve a MS SQL Server script deploy problem. It can executes script files in alphabetical order or a pre defined order. It also can ignore some errors and retry to execute scripts with a exectuon error.

You can configure a error message pattern to be ignored. The retry option can be used when some files create SQL objects that are used in other scripts.
A script may have an execution error becausa of a object that not exists but it will be create in another script.
The script with the execution error is add in a retry queue to avoid this problem.

The script execution can also be configured to execute a list of scripts listed in a configuration.

The project is organized in deploy "steps". Each deploy can have a set of configuration and child steps. The child steps share the father's configurations. Ex: You can define a connection string and share it with other script exectuion steps.

A step is a class that implements an interface. Originally a script execution step was created but other step classes were created like a git checkout step and a command prompt execution step.
