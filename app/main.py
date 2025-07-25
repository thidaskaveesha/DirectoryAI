from app.rag_engine import RAGPipeline

def main():
    rag = RAGPipeline(docs_folder="app/docs")
    rag.load_docs()

    graph = rag.build_graph()

    print("Ask a question (type 'exit' to quit):")
    while True:
        query = input("> ")
        if query.lower() in ["exit", "quit"]:
            break
        result = graph.invoke({"question": query})
        print("\n🔍 Answer:\n", result["answer"], "\n")

if __name__ == "__main__":
    main()
