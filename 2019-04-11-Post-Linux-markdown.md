= Useful commands
Doc Writer <christian.popescu@outlook.com>

Some useful commands 

== Awk commands

* *Delete leading whitespace* (spaces and tabs) from the beginning of each line.

       awk '{ sub(/^[ \t]+/, ""); print }'

* *Delete trailing whitespace* (spaces and tabs) from the end of each line.

       awk '{ sub(/[ \t]+$/, ""); print }'
       
* *Delete leading and trailing* whitespaces from each line.

       awk '{ gsub(/^[ \t]+|[ \t]+$/, ""); print }'
       
== References

http://www.dba-oracle.com/t_awk_commands.htm[Dba Oracle awk commands]
