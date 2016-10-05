using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeStatisticsCore.Collections{
    public sealed class Node{
        public string Text{
            get{
                return text;
            }
        }

        public IEnumerable<Node> Children{
            get{
                return children ?? Enumerable.Empty<Node>();
            }
        }

        private readonly string text;
        private List<Node> children;

        public Node(string text){
            this.text = text;
        }

        private void PrepareChildList(){
            if (children == null){
                children = new List<Node>();
            }
        }

        public void Add(Node node){
            PrepareChildList();
            children.Add(node);
        }

        public void Add(string nodeText){
            PrepareChildList();
            children.Add(new Node(nodeText));
        }

        public void AddRange(IEnumerable<Node> nodes){
            PrepareChildList();
            children.AddRange(nodes);
        }

        public void AddRange(IEnumerable<string> nodeTexts){
            PrepareChildList();
            children.AddRange(nodeTexts.Select(text => new Node(text)));
        }

        public void AddRangeAsStrings(IEnumerable<object> objects){
            PrepareChildList();
            children.AddRange(objects.Select(obj => new Node(obj.ToString())));
        }

        public void AddRangeAsStrings(IEnumerable objects){
            PrepareChildList();
            children.AddRange(objects.Cast<object>().Select(obj => new Node(obj.ToString())));
        }
    }
}
