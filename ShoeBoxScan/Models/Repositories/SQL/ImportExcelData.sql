INSERT INTO DATA_IMPORT_SHOE_BOX
(
    IM_SERIAL_KEY,
    TOTAL_QTY,
    PO_ID,
    UPC,
    CUSTOMER_SIZE,
    USER_ID,
    IMPORT_DATE,
    STATUS
)
VALUES
(
    :serial_key,
    :total_qty,
    :po_id,
    :upc,
    :customer_size,
    :user_id,
    to_date(:import_date, 'dd/MM/yyyy'),
    'A'
)