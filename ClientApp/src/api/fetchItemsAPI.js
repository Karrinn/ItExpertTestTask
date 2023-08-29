const url = 'item/data';

export async function getItemsDataFetch(params) {
    const response = await fetch('item/data?' + new URLSearchParams({
        code: params.code ?? '',
        value: params.value ?? '',
    }));
    if (!response.ok) {
        console.error(await response.text());
        return null;
    }
    const data = await response.json();
    return data;
}

export async function saveItemsDataFetch(jsonData) {
    const response = await fetch(
        url,
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: jsonData
        });

    if (!response.ok) {
        console.error(await response.text());
        return;
    }
}

export default {
    getItemsDataFetch,
    saveItemsDataFetch,
};