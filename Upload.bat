echo Adding new items
git add . 
set /p Message="Enter Commit Message: "
git commit -m "%Message%"
echo now pushing to master
git push -u
 