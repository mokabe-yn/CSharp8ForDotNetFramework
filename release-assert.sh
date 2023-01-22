#!/bin/bash

# CS最新ーOnly.cs の内容
# ...
# CS9-Only.cs の内容
# CS8-Only.cs の内容
# となるようにする

prev=/dev/null

while read infile; do
    outfile=${infile%-Only.cs}.cs
    cat $infile $prev > $outfile
    prev=$outfile
done < <(ls | grep -oE 'CS[0-9.]+-Only.cs' | LANG=C sort -n -t "S" -k2,2)


