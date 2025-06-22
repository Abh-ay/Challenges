CREATE TABLE FileProcessLog (
    FileProcessLogId INT IDENTITY(1,1) PRIMARY KEY,
    FileName VARCHAR(150),
    FileText VARCHAR(MAX),
    IsSuccess BIT DEFAULT 0,
    Separator VARCHAR(10) DEFAULT NULL,
    Remarks VARCHAR(MAX)
);

CREATE TABLE LinkFileWordDetail (
    LinkFileWordDetailsId INT IDENTITY(1,1) PRIMARY KEY,
    Word VARCHAR(100),
    Count INT,
    FileProcessLogId INT,
    CONSTRAINT fk_file FOREIGN KEY (FileProcessLogId)
        REFERENCES FileProcessLog(FileProcessLogId)
        ON DELETE CASCADE
);