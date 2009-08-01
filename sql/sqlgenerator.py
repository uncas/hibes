import sys


class DatabaseFormat:
    def __init__(self, name):
        self.Name = name


class SqlServerFormat(DatabaseFormat):
    def __init__(self):
        self.Name = 'SqlServer'
    CreateTableEnd = ');'
    NotNull = 'NOT NULL'
    Identity = 'IDENTITY (1, 1)'
    def GetCreateTable(self, tableName):
        return 'CREATE TABLE ' + tableName + ' ('
    def GetPrimaryKey(self, fieldName):
        return 'CONSTRAINT PK_' + fieldName + ' PRIMARY KEY CLUSTERED'
    def GetUniqueKey(self, fieldName):
        return 'CONSTRAINT UK_' + fieldName + ' UNIQUE'


class SQLiteFormat(DatabaseFormat):
    def __init__(self):
        self.Name = 'SQLite'
    CreateTableEnd = ');'
    NotNull = 'NOT NULL'
    Identity = 'IDENTITY'
    def GetCreateTable(self, tableName):
        return 'CREATE TABLE ' + tableName + ' ('
    def GetPrimaryKey(self, fieldName):
        return 'CONSTRAINT PK_' + fieldName + ' PRIMARY KEY'
    def GetUniqueKey(self, fieldName):
        return 'CONSTRAINT UK_' + fieldName + ' UNIQUE'


# Gets the sql from a line:
def GetSqlFromLine(dbFormat, line):
    sql = ''
    lineParts = line.split()
    if lineParts[0] == '+T':
        sql += dbFormat.GetCreateTable(lineParts[1])
    elif lineParts[0] == 'E':
        sql += dbFormat.CreateTableEnd
    elif lineParts[0] == '+F':
        sql += '    ' + lineParts[1]
        sql += '\n        ' + lineParts[2]
        for linePart in lineParts[3:]:
            if linePart == 'PK':
                sql += '\n        ' + dbFormat.GetPrimaryKey(lineParts[1])
            elif linePart == 'NOTNULL':
                sql += '\n        ' + dbFormat.NotNull
            elif linePart == 'UNIQUE':
                sql += '\n        ' + dbFormat.GetUniqueKey(lineParts[1])
            elif linePart == 'IDENTITY':
                sql += '\n        ' + dbFormat.Identity
    else:
        for linePart in lineParts:
            sql += linePart + ','
    return sql


def WriteSql(inputFileName, dbFormat, outputFileName):
    sql = ''
    additionalField = False
    inputFile = open(inputFileName, 'r')
    for line in inputFile:
        if len(line) > 1:
            sqlFromLine = GetSqlFromLine(dbFormat, line)
        #print('line: ' + line)
        #print('sql : ' + sqlFromLine)
            if additionalField and sqlFromLine[:4] == '    ':
                sql += '    ,' 
            sql += sqlFromLine + '\n'
            if sqlFromLine[:4] == '    ':
                additionalField = True
            else:
                additionalField = False
        else:
            additionalField = False
            sql += '\n'
    outputFile = open(outputFileName, 'w')
    outputFile.write(sql)
    print('*** ' + dbFormat.Name + ' ***')
    print(sql)


inputFileName = sys.argv[1]

dbFormat = SqlServerFormat()
outputFileName = 'sql-sql.sql'
WriteSql(inputFileName, dbFormat, outputFileName)

dbFormat = SQLiteFormat()
outputFileName = 'sql-sqlite.sql'
WriteSql(inputFileName, dbFormat, outputFileName)
