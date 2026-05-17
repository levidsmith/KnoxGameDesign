rm -rf dist
rm nespasswd.tar.gz
mkdir dist
mkdir dist/mods
cp nespasswd/.htaccess dist
cp nespasswd/main.py dist
cp -R nespasswd/mods/*.py dist/mods
cp nespasswd/requirements.txt dist
tar czvf nespasswd.tar.gz dist
