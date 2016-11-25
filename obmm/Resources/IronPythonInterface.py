import clr
from System import Array
#################################################
#		Scripting Constants		#
#################################################

#Conflict Levels
CONFLICT_MINOR 	  = ConflictLevel.MinorConflict
CONFLICT_MAJOR 	  = ConflictLevel.MajorConflict
CONFLICT_UNUSABLE = ConflictLevel.Unusable
CONFLICT_NONE 	  = ConflictLevel.NoConflict
CONFLICT_ACTIVE   = ConflictLevel.Active

CONFLICT_LEVELS   = [CONFLICT_MINOR,CONFLICT_MAJOR,CONFLICT_UNUSABLE,CONFLICT_NONE,CONFLICT_ACTIVE]

#Deactivation Status
DEACTIVE_ALLOW		= DeactiveStatus.Allow
DEACTIVE_WARN		= DeactiveStatus.WarnAgainst
DEACTIVE_DISALLOW	= DeactiveStatus.Disallow

DEACTIVE_STATUS = [DEACTIVE_ALLOW, DEACTIVE_WARN, DEACTIVE_DISALLOW]

#################################################
#		Utility Functions		#
#################################################

def ParseVersion(ver):
	if not ver:
		return [0,0,0,0]
	if type(ver) == type(Version):
		return [ver.Major, ver.Minor, ver.Build, ver.Revision]
	raise TypeError("Expected .NET class 'Version' but got " + type(ver))


#################################################
#		Script Functions		#
#################################################

def GetDisplayWarnings():
	return obmm.DisplayWarnings()

def DialogYesNo(msg, title = None):
	return obmm.DialogYesNo(msg,title)

def DataFileExists(path):
	return obmm.DataFileExists(path)

def GetOBMMVersion():
	return ParseVersion(obmm.GetOBMMVersion())
	
def GetOBSEVersion():
	return ParseVersion(obmm.GetOBSEVersion())
	
def GetOBGEVersion():
	return ParseVersion(obmm.GetOBGEVersion())
	
def GetOblivionVersion():
	return ParseVersion(obmm.GetOblivionVersion())

def GetOBSEPluginVersion(file):
	return ParseVersion(obmm.GetOBSEPluginVersion())

def GetPlugins(path, pattern="*", recurse = False):
	return list(obmm.GetPlugins(path,pattern,recurse))

def GetDataFiles(path, pattern="*", recurse = False):
	return list(obmm.GetDataFiles(path,pattern,recurse))

def GetPluginFolders(path, pattern="*", recurse = False):
	return list(obmm.PluginFolders(path,pattern,recurse))

def GetDataFolders(path, pattern="*", recurse = False):
	return list(obmm.GetDataFolders(path,pattern,recurse))

def Select( items, previews = [], descs = [], title = "", many = False):
	if( not items):
		raise Exception("Must provide a list of items to Select")
	x = [];
	for i in items:
		x.append(None)
	if(not previews): previews = x
	if(not descs): descs = x
	selected = list(obmm.Select( Array[str](items), Array[str](previews), Array[str](descs), title, many  ))
	return [x.replace("Case","",1).lstrip() for x in selected]

def Message(msg, title=""):
	obmm.Message(msg,title)

def DisplayImage(path,title=""):
	obmm.DisplayImage(path,title)

def DisplayText(path, title=""):
	obmm.DisplayText(path,title)

def LoadBefore(plugin1,plugin2):
	obmm.LoadBefore(plugin1,plugin2)

def LoadAfter(plugin1, plugin2):
	obmm.LoadAfter(plugin1,plugin2)

def UncheckEsp(plugin):
	obmm.UncheckEsp(plugin)

def SetDeactivationWarning( plugin, warning):
	if warning not in DEACTIVE_STATUS:
		raise TypeError("Warning must be a deactivation stats, one of: " + str(DEACTIVE_STATUS) )
	obmm.SetDeactivationWarning(plugin)

def ConflictsWithRegex( filename,  comment = "", level = CONFLICT_MINOR, minVersion = None, maxVersion = None):
	ConflictsWith(filename,comment,level,minVersion,maxVersion,True)

def ConflictsWith( filename,  comment = "", level = CONFLICT_MINOR, minVersion = None, maxVersion = None, regex = False):
	if level not in CONFLICT_LEVELS:
		raise TypeError("level must be a conflict level, one of: " + str(CONFLICT_LEVELS) )
	if( minVersion and maxVersion):
		obmm.ConflictsWith(filename,minVersion[0],minVersion[1],maxVersion[0],maxVersion[1],comment,level,regex)
	else:
		obmm.ConflictsWith(filename,0,0,0,0,comment,level,regex)
			
def DependsOnRegex( filename,  comment = "", minVersion = None, maxVersion = None):
	DependsOn(filename,comment,minVersion,maxVersion,True)
	
def DependsOn( filename,  comment = "", minVersion = None, maxVersion = None, regex = False):
	if( minVersion and maxVersion):
		obmm.DependsOn(filename,minVersion[0],minVersion[1],maxVersion[0],maxVersion[1],comment,regex)
	else:
		obmm.DependsOn(filename,0,0,0,0,comment,regex)

def DontInstallAnyPlugins():
	obmm.DontInstallAnyPlugins()
	
def DontInstallAnyDataFiles():
	obmm.DontInstallAnyDataFiles()

def InstallAllPlugins():
	obmm.InstallAllPlugins()

def InstallAllDataFiles():
	obmm.InstallAllDataFiles()

def DontInstallPlugin(name):
	obmm.DontInstallPlugin(name)

def DontInstallDataFile(name):
	obmm.DontInstallDataFile(name)
	
def DontInstallDataFolder(folder, recurse = False):
	obmm.DontInstallDataFolder(folder,recurse)

def InstallPlugin(name):
	obmm.InstallPlugin(name)

def InstallDataFile(name):
	obmm.InstallDataFile(name)

def InstallDataFolder(folder, recurse = False):
	obmm.InstallDataFolder(folder,recurse)

def CopyPlugin( frm ,  to):
	obmm.CopyPlugin(frm,to)

def CopyDataFile( frm ,  to):
	obmm.CopyDataFile(frm,to)

def CopyDataFolder( frm ,  to, recurse = False):
	obmm.CopyDataFolder(frm,to,recurse)

def PatchPlugin( frm,  to, create = False):
	obmm.PatchPlugin(frm,to,create)

def PatchDataFile( frm,  to, create = False):
	obmm.PatchDataFile(frm,to,creat)

def RegisterBSA( path):
	obmm.RegisterBSA(path)

def UnregisterBSA( path):
	obmm.UnregisterBSA(path)

def EditINI( section,  key,  value):
	obmm.EditINI(section,key,value)

def EditShader(package,  name,  path):
	obmm.EditINI(package,name,path)

def FatalError():
	obmm.FatalError()

def SetGMST( file,  edid,  value):
	obmm.SetGMST(file,edid,value)

def SetGlobal( file,  edid,  value):
	obmm.SetGlobal(file,edid,value)

def SetPluginByte( file, offset, value):
	obmm.SetPluginByte( file, offset, value)
	
def SetPluginShort( file, offset, value):
	obmm.SetPluginShort( file, offset, value)
	
def SetPluginInt( file, offset, value):
	obmm.SetPluginInt( file, offset, value)
	
def SetPluginLong( file, offset, value):
	obmm.SetPluginLong( file, offset, value)
	
def SetPluginFloat( file, offset, value):
	obmm.SetPluginFloat( file, offset, float(value) )

def InputString( title = "", initial = ""):
	return obmm.InputString(title,initial)

def ReadINI( section,  value):
	return obmm.ReadINI(section,value)

def ReadRendererInfo( value):
	return obmm.ReadRenderInfo()

def EditXMLLine( file,  line,  value):
	return obmm.EditXMLLine(file,line,value)

def EditXMLReplace( file,  find,  replace):
	return obmm.EditXMLReplace(file,find,replace)

def CreateCustomDialog():
	return obmm.CreateCustomDialog()

def ReadDataFile(file):
	return obmm.ReadDataFile(file)
	
def ReadExistingDataFile(file):
	return obmm.ReadExistingDataFile(file)
	
def GetDataFileFromBSA(file):
	return obmm.GetDataFileFromBSA(file)

def GetDataFileFromBSA(bsa, file):
	return obmm.GetDataFileFromBSA(bsa, file)

def GenerateNewDataFile(file, data):
	return obmm.GenerateNewDataFile(file, data)
	
def GenerateBSA(file, path):
	return obmm.GenerateBSA(file, path)
	
def IsSimulation():
	return obmm.IsSimulation()

#################################################
#	Remove Unsafe Functions			#
#################################################

import __builtin__
try:
	del __builtin__.compile
	del __builtin__.eval
	del __builtin__.execfile
	del __builtin__.file
	del __builtin__.open
	del __builtin__.reload
except:
	pass
