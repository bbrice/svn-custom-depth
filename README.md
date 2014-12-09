svn-custom-depth
================
A utility for determining if directories in a SVN checkout have custom depth
information.

The current project page is located here:
<https://github.com/bbrice/svn-custom-depth>

Motivation
----------
Sometimes I had the desire to checkout a whole SVN repository, but selectively
"forget" about certain subdirectories.  For whatever reason, I chose that I
didn't care about files in that subdirectory.  I could change the SVN depth
on that directory to ignore changes in that directory:
`svn update --set-depth exclude FILE`.  Some time later I would forget which
files I had set custom depths on, so I made this utility for helping me find
this information again.


Building
--------
At the moment, Visual Studio 2013.4 is used for this project.

Usage
-----
This application can take directory paths as arguments.  If no path is given,
then the current directory is used.

	gaia:~$ svn-custom-depth svn-project
	/home/bbrice/svn-project/large-dir
		exclude
	/home/bbrice/svn-project/large-file.zip
		exclude

License
-------
`svn-custom-depth` is licensed under the BSD license. See
[LICENSE.txt](LICENSE.txt) for more info.
