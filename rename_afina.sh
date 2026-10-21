#!/bin/bash

# Set to 1 to just preview the changes without actually renaming anything.
# Set to 0 to perform the actual renames.
DRY_RUN=1

echo "Starting rename process (Files & Directories)..."
echo "Replacing 'AfinApp' with 'ToDoApp'..."
echo "---------------------------------------------------"

# -depth ensures we rename children before their parent directories!
# Omitting -type f/d means it will find BOTH files and directories.
find . -depth -iname "*ToDoApp*" -print0 | while IFS= read -r -d '' path; do

    # Get the directory part and the base name
    dir=$(dirname "$path")
    base=$(basename "$path")

    # Replace 'AfinApp' with 'ToDoApp' (case-sensitive)
    new_base="${base//AfinApp/ToDoApp}"

    # Only proceed if the name actually changed
    if [[ "$base" != "$new_base" ]]; then
        new_path="$dir/$new_base"

        # Safety check: prevent overwriting existing files/directories
        if [[ -e "$new_path" ]]; then
            echo "SKIPPED (Target exists): $path -> $new_path"
        else
            if [[ "$DRY_RUN" -eq 1 ]]; then
                echo "[DRY RUN] WOULD RENAME: $path -> $new_path"
            else
                mv "$path" "$new_path"
                echo "RENAMED: $path -> $new_path"
            fi
        fi
    fi
done

echo "---------------------------------------------------"
if [[ "$DRY_RUN" -eq 1 ]]; then
    echo "Dry run complete. No files were changed."
    echo "Change DRY_RUN=0 in the script to apply changes."
else
    echo "Rename process complete."
fi
