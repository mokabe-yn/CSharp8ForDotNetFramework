#!/usr/bin/python3

import os
import sys

import itertools

# end_of_line: OS default
# trim_trailing_whitespace
# insert_final_newline

def main(fname: str):
    with open(fname, "r", encoding="utf8") as f:
        data=f.read()
    lines=data.splitlines()
    
    # 余計な末尾改行を削除
    lines.reverse()
    lines=list(itertools.dropwhile(lambda x:not x, lines))
    lines.reverse()
    
    # 行終端のスペース削除
    lines=list(map(str.rstrip, lines))
    
    with open(fname, "w", encoding="utf8") as f:
        f.writelines(map(lambda x:x+"\n", lines))

if __name__ == "__main__":
    for x in sys.argv[1:]:
        main(x)

