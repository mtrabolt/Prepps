import {
    Box, Button, CircularProgress, Container, Grid, Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TableSortLabel, TextField,
} from "@mui/material";
import {NextPage} from "next";
import React, {useEffect, useState} from "react";
import {visuallyHidden} from "@mui/utils";
import {useProducts} from "../../hooks/useProducts";
import EditIcon from '@mui/icons-material/Edit';
import {RemoveCircleOutlined} from "@mui/icons-material";
import {Product, ProductWriteModel} from "../../Models/Product";

type Order = 'asc' | 'desc';

function descendingComparator<T>(a: T, b: T, orderBy: keyof T) {
    if (b[orderBy] < a[orderBy]) {
        return -1;
    }
    if (b[orderBy] > a[orderBy]) {
        return 1;
    }
    return 0;
}

function getComparator<Key extends keyof Product>(
    order: Order,
    orderBy: Key,
): (
    // a: { [key in Key]: string },
    // b: { [key in Key]: string },
    a: Product,
    b: Product,
) => number {
    return order === 'desc'
        ? (a, b) => descendingComparator(a, b, orderBy)
        : (a, b) => -descendingComparator(a, b, orderBy);
}

interface HeadCell {
    disablePadding: boolean;
    id: keyof Product;
    label: string;
    numeric: boolean;
}

const headCells: readonly HeadCell[] = [
    {
        id: 'name',
        numeric: false,
        disablePadding: true,
        label: 'Name',
    },
    {
        id: 'expiresAt',
        numeric: false,
        disablePadding: false,
        label: 'Expires At',
    },
    {
        id: 'isExpired',
        numeric: false,
        disablePadding: false,
        label: 'Is Expired',
    },
    // {
    //     id: 'actions',
    //     numeric: false,
    //     disablePadding: false,
    //     label: '',
    // }
]

// const createSortHandler =
//     (property: keyof Product) => (event: React.MouseEvent<unknown>) => {
//         onRequestSort(event, property);
//     };

const Products: NextPage = () => {
    const {products: initialProducts, isError, isLoading} = useProducts();

    const [order, setOrder] = useState<Order>('asc');
    const [orderBy, setOrderBy] = useState<keyof Product>('name');

    let today = new Date().toString();

    const [products, setProducts] = useState<Product[]>([]);
    const [newProductName, setNewProductName] = useState("");
    const [newProductDate, setNewProductDate] = useState(today);
    
    useEffect(() => {setProducts(initialProducts)}, [isLoading]);
    
    if (isLoading) return (
        <Grid
            container
            spacing={0}
            direction="column"
            alignItems="center"
            justifyContent="center"
            style={{ minHeight: '100vh' }}
        >
            <Grid item xs={3}>
                <CircularProgress />
            </Grid>
        </Grid>
    );
    if (isError) return (<h1>{{isError}}</h1>);

    const deleteProduct = async (id: string) => {
        await fetch(`api/Products/` + id, {
            method: "delete",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        })
            .then(() => setProducts(products.filter((p) => p.id !== id)));
    }
    
    const createNewProduct = async ()  => {
        let productWriteModel: ProductWriteModel = {
            name: newProductName,
            expiresAt: newProductDate,
        };
        
        await fetch(`api/Products`,{
            method: "post",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productWriteModel),
        })
        .then((res) => res.text())
        .then((resultString) => {
            let newProduct: Product = JSON.parse(resultString)
            setProducts([newProduct, ...products]);
            
            setNewProductName('');
            setNewProductDate(today);
        });
    };
    
    return (
        <>
        <Container maxWidth="sm">
            <h1>
                Products
            </h1>

            <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            {headCells.map((headCell) => (
                                <TableCell
                                    key={headCell.id}
                                    align={'left'}
                                    padding={headCell.disablePadding ? 'none' : 'normal'}
                                    sortDirection={orderBy === headCell.id ? order : false}
                                >
                                    <TableSortLabel
                                        active={orderBy === headCell.id}
                                        direction={orderBy === headCell.id ? order : 'asc'}
                                        // onClick={createSortHandler(headCell.id)}
                                    >
                                        {headCell.label}
                                        {orderBy === headCell.id ? (
                                            <Box component="span" sx={visuallyHidden}>
                                                {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                            </Box>
                                        ) : null}
                                    </TableSortLabel>
                                </TableCell>
                            ))}
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            products.slice().sort(getComparator(order, orderBy))
                                .map((row, index) => {
                                    const labelId = `enhanced-table-checkbox-${index}`;
                                    
                                    return (
                                        <TableRow
                                            hover
                                            // onClick={(event) => handleClick(event, row.name)}
                                            role="checkbox"
                                            // aria-checked={isItemSelected}
                                            tabIndex={-1}
                                            key={row.name + index}
                                            // selected={isItemSelected}
                                        >
                                            {/*<TableCell padding="checkbox">*/}
                                            {/*    <Checkbox*/}
                                            {/*        color="primary"*/}
                                            {/*        checked={isItemSelected}*/}
                                            {/*        inputProps={{*/}
                                            {/*            'aria-labelledby': labelId,*/}
                                            {/*        }}*/}
                                            {/*    />*/}
                                            {/*</TableCell>*/}
                                            <TableCell
                                                component="th"
                                                id={labelId}
                                                scope="row"
                                                align="right"
                                            >
                                                {row.name}
                                            </TableCell>
                                            <TableCell align="right">{row.expiresAt}</TableCell>
                                            <TableCell align="right">{row.isExpired ? "Yes" : "No"}</TableCell>
                                            <TableCell align="center">
                                                <Button>
                                                    <EditIcon></EditIcon>
                                                </Button>
                                                <Button onClick={async () => {
                                                    await deleteProduct(row.id);
                                                }}>
                                                    <RemoveCircleOutlined></RemoveCircleOutlined>
                                                </Button>
                                            </TableCell>
                                        </TableRow>
                                    );
                                })
                        }
                            <TableRow
                                hover
                                role="checkbox"
                                // aria-checked={isItemSelected}
                                tabIndex={-1}
                            >
                                <TableCell align="right">
                                    <TextField 
                                        id="create-product-name" 
                                        label="Name" 
                                        value={newProductName} 
                                        variant="standard"
                                        onKeyUp={ async ({code}) => {
                                            code === 'Enter' ? await createNewProduct() : null
                                        }}
                                        onChange={((e) => setNewProductName(e.target.value))}
                                    />
                                </TableCell>
                                <TableCell align="right">
                                    <TextField
                                        id="create-product-expiration-date"
                                        label="Expiration date"
                                        type="date"
                                        variant="standard"
                                        // value={newProductDate}
                                        defaultValue={newProductDate}
                                        onKeyUp={async ({code}) => {
                                            code === 'Enter' ? await createNewProduct() : null
                                        }}
                                        onChange={(({target}) => {
                                            setNewProductDate(target.value)
                                        })}
                                        sx={{ width: 220 }}
                                        InputLabelProps={{
                                            shrink: true,
                                        }}
                                    />
                                </TableCell>
                                <TableCell align="right">
                                    <Button
                                        variant="outlined"
                                        onClick={async () => {await createNewProduct()}}
                                    >
                                        Save
                                    </Button>
                                </TableCell>
                                
                            </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
        {/*<Box*/}
        {/*    sx={{*/}
        {/*        margin: 0,*/}
        {/*        top: 'auto',*/}
        {/*        right: 20,*/}
        {/*        bottom: 20,*/}
        {/*        left: 'auto',*/}
        {/*        position: 'fixed',*/}
        {/*    }}*/}
        {/*>*/}
        {/*    <Fab color="default" aria-label="add">*/}
        {/*        <AddIcon />*/}
        {/*    </Fab>*/}
        {/*</Box>*/}
        </>
    )
}

export default Products
