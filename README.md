# Description

A Simple Unique Identifier (SUID) is (almost) unique like a GUID but only 7 characters !

There are five types of fixed size SUID : 
* LettersOnlySuid : 14 lowercase letters
* UrlFriendlySuid : 10 url friendly characters
* FilenameSuid : 10 characters than can be used to create a filename
* Suid : 9 printable characters
* TinySuid : 7 copy pastable characters

There are also extensions to convert a guid into a Suid (20 characters) or a TinySuid (16 characters).
By copy pastable, it means that it can be visible with a proper font (like Arial or Segoe UI)

Collision : Is it possible to create two Suid that are the same ? Yes and no.
For Suids in only one instance of the generator, you can't get the same unique identifier for more than 160 years (in 2178). Exception is if you create more than 65535 suids in the same second (1 every 15 µs) you could possibly get a duplicate. In fact, there is 1/256 chance to get a collision on the 65536th created suid in the same second.

For Suids in multiple instances, chance is 1 / 16 777 216 if created in the same second. So, Suid is NOT recommended if you create multiple suids in multiple instances that need to stay unique without any possible verification of uniqueness. If you use Suid that way, I recommend using the Guid extension ToTinySuid() (16 characters) or ToSuid() (20 characters) from a real guid. It will be as unique as a guid is.

# Usage
## Install package
```shell
dotnet add package rlcx.suid
```

## Generate a SUID
```cs
var id = Suid.NewSuid();
```
This will generate a Suid made of 9 printable characters.


# Examples
Here is an example of 10 tiny suids generated in a for loop without sleeps between.
```
iↁΛ₆#~a
jↁ&₆#~a
kↁé₆#~a
lↁM₆#~a
mↁ;₆#~a
nↁ'₆#~a
oↁ⅖₆#~a
pↁj₆#~a
qↁ®₆#~a
rↁↁ₆#~a
```
As you can see, these are unique but with very strange characters :)

## Suids
```
o+am^9W8!
oG)uB9W8!
obCcs9W8!
p(dJh9W8!
pD)NK9W8!
p_B^k9W8!
q%[,u9W8!
q@tpQ9W8!
q\B"m9W8!
r"Tt29W8!
```
Better, but longer :)

## Others
 * LettersOnlySuid : `jmobpkojenckaa`, `jnobepojenckaa`, `joobfdojenckaa`, `jpobmbojenckaa`, `kaobmgojenckaa`
 * UrlFriendlySuid : `vN+o6U1KAA`, `vd/M6U1KAA`, `vt+P6U1KAA`, `v9/d6U1KAA`, `wN+j6U1KAA`
 * FilenameSuid : `tgte°)#d00`, `tws!°)#d00`, `u0vh°)#d00`, `uàuñ°)#d00`, `ugu°°)#d00`