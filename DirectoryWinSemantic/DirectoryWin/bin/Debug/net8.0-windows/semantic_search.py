import sys
import os
from sentence_transformers import SentenceTransformer, util
from thefuzz import fuzz

def read_file_content(file_path):
    try:
        if file_path.endswith(".txt"):
            with open(file_path, 'r', encoding='utf-8') as f:
                return f.read()
        elif file_path.endswith(".docx"):
            import docx
            return "\n".join([p.text for p in docx.Document(file_path).paragraphs])
        elif file_path.endswith(".pdf"):
            from PyPDF2 import PdfReader
            reader = PdfReader(file_path)
            return "\n".join([page.extract_text() or '' for page in reader.pages])
    except Exception as e:
        print(f"[ERROR] {file_path} => {e}", file=sys.stderr)
    return ""

def main():
    if len(sys.argv) != 3:
        print("Usage: semantic_search.py <keyword> <file_list_path>")
        sys.exit(1)

    keyword = sys.argv[1].strip().lower()
    file_list_path = sys.argv[2]

    with open(file_list_path, 'r', encoding='utf-8') as f:
        files = [line.strip() for line in f if line.strip()]

    # import ssl
    # ssl._create_default_https_context = ssl._create_unverified_context

    model = SentenceTransformer('all-MiniLM-L6-v2')
    keyword_vec = model.encode(keyword, convert_to_tensor=True)

    results = []

    for file in files:
        if not os.path.exists(file):
            continue

        content = read_file_content(file)
        content_clean = content.strip().lower()

        if not content_clean:
            continue  # Skip empty or unreadable files

        # Semantic similarity score
        doc_vec = model.encode(content_clean, convert_to_tensor=True)
        sim_score = util.cos_sim(keyword_vec, doc_vec).item()

        # Fuzzy matching score (based on text similarity)
        fuzzy_score = fuzz.partial_ratio(keyword, content_clean)

        # Keep if either score is above threshold
        if sim_score > 0.3 or fuzzy_score > 70:
            results.append((file, sim_score, fuzzy_score))

    results.sort(key=lambda x: x[1], reverse=True)  # Sort by semantic score

    for file, sim_score, fuzzy_score in results:
        print(f"{file}|||{sim_score:.4f}|||fuzzy:{fuzzy_score}")

if __name__ == "__main__":
    main()
