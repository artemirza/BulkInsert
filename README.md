# BulkInsert
**How to Use**

Setup:
Ensure MS SQL Server is running and configured.
Create the required database and table using the provided SQL schema.

Run the Program:
Place the input CSV file in the desired directory.
Run the CLI tool with the file path and desired output paths for duplicates.
Output:

Processed data will be inserted into the database.
Duplicates will be logged into the specified duplicates.csv file.

**Solution to point #4**

Create indexes to work with Index Scan
CREATE INDEX IDX_PULocationID ON Trips (PULocationID);
CREATE INDEX IDX_TripDistance ON Trips (trip_distance DESC);
CREATE INDEX IDX_TravelTime ON Trips (tpep_pickup_datetime, tpep_dropoff_datetime);

1) Find out which `PULocationId` (Pick-up location ID) has the highest tip_amount on average. - 194
SELECT TOP 1 PULocationID, AVG(tip_amount) AS AvgTip
FROM Trips
GROUP BY PULocationID
ORDER BY AvgTip DESC;

2) Find the top 100 longest fares in terms of `trip_distance`.
SELECT TOP 100 *
FROM Trips
ORDER BY trip_distance DESC;

3) Find the top 100 longest fares in terms of time spent traveling.
SELECT TOP 100 *,
       DATEDIFF(SECOND, tpep_pickup_datetime, tpep_dropoff_datetime) AS TravelTime
FROM Trips
ORDER BY TravelTime DESC;

4) Search, where part of the conditions is `PULocationId`.
DECLARE @PULocationID INT = 4;

SELECT *
FROM Trips
WHERE PULocationID = @PULocationID;

**Solution to point #9**
To handle large files efficiently, the program should read the file in batches (It can be 10000 rows) rather than loading the entire file into memory. Each batch can then be processed and inserted into the database before moving to the next batch. 

**Deliverables:**
Number of rows in the table after running the program is 29889. Csv file has 111 dublicates.

**Assumptions:** 
I tried to implement the work with batches according to my interest, but I had a problem with the detection of duplicates, because I checked duplicates also with batches, I faced exceptions (the row with this key is already in the database) because I could not know which row I had already written to the database in the previous iteration. 
