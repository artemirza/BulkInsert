# BulkInsert Project

## Overview
The BulkInsert project is a CLI-based ETL (Extract, Transform, Load) tool designed to import trip data from a CSV file into an MS SQL Server database efficiently. It removes duplicate entries, transforms data where necessary, and optimizes the database schema for common queries.

---

## How to Use

### Setup
1. Ensure MS SQL Server is installed and running.
2. Create the required database and table using the provided SQL schema.

### Running the Program
1. Place the input CSV file in the desired directory.
2. Run the CLI tool with the file path and desired output paths for duplicates.

### Output
- Processed data will be inserted into the database.
- Duplicate records will be logged into the specified `duplicates.csv` file.

---

## Solution to Query Optimization (Point #4)

### Indexing
To optimize queries, create the following indexes:

```sql
CREATE INDEX IDX_PULocationID ON Trips (PULocationID);
CREATE INDEX IDX_TripDistance ON Trips (trip_distance DESC);
CREATE INDEX IDX_TravelTime ON Trips (tpep_pickup_datetime, tpep_dropoff_datetime);

```sql
SELECT TOP 1 PULocationID, AVG(tip_amount) AS AvgTip
FROM Trips
GROUP BY PULocationID
ORDER BY AvgTip DESC;

```sql
SELECT TOP 100 *
FROM Trips
ORDER BY trip_distance DESC;

```sql
SELECT TOP 100 *,
       DATEDIFF(SECOND, tpep_pickup_datetime, tpep_dropoff_datetime) AS TravelTime
FROM Trips
ORDER BY TravelTime DESC;

```sql
DECLARE @PULocationID INT = 4;

SELECT *
FROM Trips
WHERE PULocationID = @PULocationID;

### Handling Large Files (Point #9)

To process large files efficiently:

- **Batch Processing**: Read the CSV file in manageable batches (e.g., 10,000 rows per batch) to prevent memory overload.
- **Incremental Insertion**: Process each batch individually and insert it into the database before moving to the next batch.
- **Optimized Duplicate Detection**: Track records that have already been inserted into the database to ensure accurate duplicate handling across batches.

---

### Deliverables

- **Rows in Database**: 29,889 records successfully inserted.
- **Duplicates Identified**: 111 records logged as duplicates.

---

### Assumptions and Challenges

- **Batch Processing Limitations**: Initial attempts to detect duplicates within batches introduced challenges with inter-batch duplicate tracking. This led to exceptions when inserting records that already existed from previous batches.
- **Simplified Approach**: The current implementation processes the entire dataset in memory for simplicity, ensuring accurate duplicate detection and avoiding inter-batch inconsistencies.
