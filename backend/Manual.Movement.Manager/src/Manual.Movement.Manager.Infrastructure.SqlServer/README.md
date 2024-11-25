# 📊 Database Structure: Financial Transactions

This document describes the database schema for the **Financial Transactions** project. It includes details about the tables, relationships, and constraints.

---

## 🗂️ Database: `FINANCIAL_TRANSACTIONS`

### **1. Table: `PRODUTO`**
Represents products with basic information.

#### Columns:
| Column Name   | Data Type      | Nullability | Description                          |
|---------------|----------------|-------------|--------------------------------------|
| `COD_PRODUTO` | `char(4)`      | NOT NULL    | Unique identifier for the product.  |
| `DES_PRODUTO` | `varchar(30)`  | NULL        | Description of the product.         |
| `STA_STATUS`  | `char(1)`      | NULL        | Status of the product.              |

#### Constraints:
- **Primary Key**: `PK_dbo.PRODUTO`
  - Columns: `COD_PRODUTO`

---

### **2. Table: `PRODUTO_COSIF`**
Maps products to COSIF codes with classification.

#### Columns:
| Column Name        | Data Type      | Nullability | Description                          |
|--------------------|----------------|-------------|--------------------------------------|
| `COD_PRODUTO`      | `char(4)`      | NOT NULL    | Foreign key referencing `PRODUTO`.  |
| `COD_COSIF`        | `char(11)`     | NOT NULL    | Unique COSIF code.                  |
| `COD_CLASSIFICACAO`| `char(6)`      | NULL        | Classification code.                |
| `STA_STATUS`       | `char(1)`      | NULL        | Status of the COSIF mapping.        |

#### Constraints:
- **Primary Key**: `PK_dbo.PRODUTO_COSIF`
  - Columns: `COD_PRODUTO`, `COD_COSIF`
- **Foreign Key**: `FK_dbo.PRODUTO_COSIF_dbo.PRODUTO_COD_PRODUTO`
  - References: `PRODUTO(COD_PRODUTO)`

---

### **3. Table: `MOVIMENTO_MANUAL`**
Records manual financial movements.

#### Columns:
| Column Name       | Data Type       | Nullability | Description                          |
|-------------------|-----------------|-------------|--------------------------------------|
| `DAT_MES`         | `int`           | NOT NULL    | Month of the movement.              |
| `DAT_ANO`         | `int`           | NOT NULL    | Year of the movement.               |
| `NUM_LANCAMENTO`  | `bigint`        | NOT NULL    | Sequential number for the movement. |
| `COD_PRODUTO`     | `char(4)`       | NOT NULL    | Foreign key referencing `PRODUTO`.  |
| `COD_COSIF`       | `char(11)`      | NOT NULL    | Foreign key referencing `PRODUTO_COSIF`. |
| `DES_DESCRICAO`   | `varchar(50)`   | NOT NULL    | Description of the movement.        |
| `DAT_MOVIMENTO`   | `datetime`      | NOT NULL    | Timestamp of the movement.          |
| `COD_USUARIO`     | `varchar(15)`   | NOT NULL    | User code who performed the movement. |
| `VAL_VALOR`       | `decimal(18,2)` | NOT NULL    | Value of the movement.              |

#### Constraints:
- **Primary Key**: `PK_dbo.MOVIMENTO_MANUAL`
  - Columns: `DAT_MES`, `DAT_ANO`, `NUM_LANCAMENTO`, `COD_PRODUTO`, `COD_COSIF`
- **Foreign Key**: `FK_dbo.MOVIMENTO_MANUAL_dbo.PRODUTO_COSIF_COD_PRODUTO_COD_COSIF`
  - References: `PRODUTO_COSIF(COD_PRODUTO, COD_COSIF)`

---

## 🔗 Relationships
1. **`PRODUTO` ↔ `PRODUTO_COSIF`**:
   - `PRODUTO.COD_PRODUTO` → `PRODUTO_COSIF.COD_PRODUTO`

2. **`PRODUTO_COSIF` ↔ `MOVIMENTO_MANUAL`**:
   - `(PRODUTO_COSIF.COD_PRODUTO, PRODUTO_COSIF.COD_COSIF)` → `(MOVIMENTO_MANUAL.COD_PRODUTO, MOVIMENTO_MANUAL.COD_COSIF)`
