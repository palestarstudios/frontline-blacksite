#!/usr/bin/env python3
#
# bz2compress.py - compress your assets in a flash!
# (c) 2026 Pale Star Studios

import os
import bz2
import hashlib

def build_assets(source_path: str, output_path: str):
    if not os.path.exists(output_path):
        os.makedirs(output_path)

    for filename in os.listdir(source_path):
        if filename.endswith(".txt"):
            raw_path = os.path.join(source_path, filename)
            compressed_path = os.path.join(output_path, filename + ".bz2")
            key_path = os.path.join(output_path, filename + ".key")

            with open(raw_path, "rb") as src, bz2.open(compressed_path, "wb") as dest:
                dest.write(src.read())

            sha = hashlib.sha384()
            with open(compressed_path, "rb") as f:
                sha.update(f.read())

            with open(key_path, "w") as k:
                k.write(sha.hexdigest())

            print(f"Locked file \"{filename}\" with key \"{sha}\"")

if __name__ == "__main__":
    source_pt = input("Provide path to files: ")
    output_pt = input("Output results to: ")

    build_assets(source_pt, output_pt)
    print("Files are secured!")
else:
    pass
