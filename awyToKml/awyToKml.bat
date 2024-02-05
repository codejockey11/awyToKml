DEL awyRoute*.kml

DEL awyToKml.txt

DEL params.sql

REM selecting by airway
REM SET airway=V38
SET airway=0

if [%airway%] NEQ [0] (
ECHO SET @airway='%airway%'; > params.sql
TYPE awyToKmlAirway.sql >> params.sql

mysql.exe --login-path=batch --table < params.sql

awyToKml.exe awyToKml.txt %airway%

REM CALL :SetOgr2OgrPath
REM CALL :SingleRoute

DEL params.sql

GOTO :CLOSE
)

if [%airway%] EQU [0] (
mysql.exe --login-path=batch --table < awyToKml.sql

awyToKml.exe awyToKml.txt %airway%

REM CALL :SetOgr2OgrPath
REM CALL :AllRoutes

GOTO :CLOSE
)

:SetOgr2OgrPath
SET GDAL_DATA=C:\\Program Files\\QGIS 3.22.1\\apps\\gdal-dev\\data
SET GDAL_DRIVER_PATH=C:\\Program Files\\QGIS 3.22.1\\bin\\gdalplugins
SET OSGEO4W_ROOT=C:\\Program Files\\QGIS 3.22.1
SET PATH=%OSGEO4W_ROOT%\\bin;%PATH%
SET PYTHONHOME=%OSGEO4W_ROOT%\\apps\\Python37
SET PYTHONPATH=%OSGEO4W_ROOT%\\apps\\Python37

EXIT /B

:SingleRoute
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\route%airway%.shp" "route%airway%.kml" route%airway%

EXIT /B

:AllRoutes
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeA.shp" "routeA.kml" routeA
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeAT.shp" "routeAT.kml" routeAT
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeB.shp" "routeB.kml" routeB
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeBF.shp" "routeBF.kml" routeBF
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeG.shp" "routeG.kml" routeG
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeJ.shp" "routeJ.kml" routeJ
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routePA.shp" "routePA.kml" routePA
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routePR.shp" "routePR.kml" routePR
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeQ.shp" "routeQ.kml" routeQ
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeR.shp" "routeR.kml" routeR
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeT.shp" "routeT.kml" routeT
ogr2ogr.exe -f "ESRI Shapefile" -skipfailures "shape\\routeV.shp" "routeV.kml" routeV

EXIT /B

:CLOSE

REM DEL awyToKml.txt

:EOF
