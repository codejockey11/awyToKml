USE aviation;

SELECT *
	INTO OUTFILE 'C:\\Users\\junk_\\Documents\\qgisBatch\\awyToKml.txt' FIELDS TERMINATED BY '~' LINES TERMINATED BY '\r\n'
	FROM airway;
